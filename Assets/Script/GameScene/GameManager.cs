using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    private const float startDelay = 1f;
    [SerializeField] private GameObject readyUIObject;
    public AudioSource audioSource;
    public static GameManager Instance { get; private set; } // 싱글톤 패턴으로 구현하여 다른 클래스에서 쉽게 접근 가능

    // 각종 매니저 및 컨트롤러 참조
    // 클래스 간의 의존성을 줄이기 위해, GameManager가 필요한 매니저와 컨트롤러를 직접 참조하도록 설계
    public JSONConverter jsonConverter;
    public ChartDataViewer chartDataViewer;
    public ChartScheduler chartScheduler;
    public GameTester gameTester;

    public LaneController laneController;
    public JudgeSystem judgeSystem;
    public MusicPlayer musicPlayer;
    public NoteManager noteManager;
    public EnemyManager enemyManager;
    public BossManager bossManager;
    public TextEffectManager textEffectManager;
    public NoteEffectManager noteEffectManager;
    public AlertEffectManager alertEffectManager;
    public BPMEventManager bpmEventManager;
    public BeatLineManager beatLineManager;

    public UIManager uiManager;
    public PlayerController playerController;
    public ResultManager resultManager;

    public Transform lane;

    public float progress; // 스토리 진행도

    public float bpm; // 곡의 BPM
    public int resolution; // 곡의 해상도. 한 박을 몇 개의 tick으로 나누는지
    // 주로 480을 사용. 480의 약수가 많기 때문에 다양한 박자를 표현 가능. 예를 들어 노트를 4개로 나누려면 480 / 4 = 120 tick마다 노트를 배치.
    public float audioOffset; // sec 단위, 음악 재생 지연 (양수: 음악 지연, 음수: 음악 앞당김)

    public float arriveTime; // sec 단위, 노트가 판정 지점에 도달하는 시간
    public float arriveTick; // tick 단위, 노트가 판정 지점에 도달하는 tick

    public float perfectTime; // ms 기준, Perfect 판정 범위
    public float goodTime; // ms 기준, Good 판정 범위
    public float missTime; // ms 기준, Miss 판정 범위
    public float noteSpeed; // 노트 이동 속도 배율

    public float perfectTick { get; private set; }
    public float goodTick { get; private set; }
    public float missTick { get; private set; }
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void Init(string songName)
    {
        Debug.Log($"[GameManager] SongName : {songName}");
        // Test Code - JSON 파일에서 ChartData 로드
        jsonConverter.Init();
        ChartData chartData = jsonConverter.Load(songName);
        //Debug.Log("[GameManager]ChartData loaded: " + chartData.metaData.title);
        // chartDataViewer.ViewChartData(chartData);

        bpm = chartData.metaData.bpm;
        resolution = chartData.metaData.resolution;
        audioOffset = chartData.metaData.offset; // (양수: 음악 지연, 음수: 음악 앞당김)
        //Debug.Log("[GameManager] Start: bpm = " + bpm + ", resolution = " + resolution + ", offset = " + audioOffset);

        laneController.Init(noteSpeed); // LaneController 초기화, noteSpeed 전달
        arriveTime = laneController.getArriveTime() + startDelay; // LaneController로부터 arriveTime 계산

        musicPlayer.Init(chartData.metaData.title, arriveTime, audioOffset); // MusicPlayer 초기화, arriveTime과 audioOffset 전달
        TickClock.Instance.Init(bpm, resolution); // TickClock 초기화, bpm과 resolution 전달
        Debug.Log($"[GameManager] arriveTime: {arriveTime}, audioOffset: {audioOffset}");

        arriveTick = arriveTime * (bpm / 60f) * resolution;
        noteManager.Init(resolution, noteSpeed, arriveTick, laneController.moveDistance); // NoteSpawner 초기화, resolution과 noteSpeed, arriveTick, moveDistance 전달
        float endTick = musicPlayer.ClipLength * (bpm / 60f) * resolution;
        float startTick = chartData.metaData.startTick;
        beatLineManager.Init(resolution, noteSpeed, arriveTick, laneController.moveDistance, startTick, endTick);
        enemyManager.Init(); // EnemySpawner 초기화
        alertEffectManager.Init();

        chartScheduler.Init(chartData); // ChartScheduler 초기화, ChartData 전달

        gameTester.Jump(); // 특정 구간으로 점프. 개발용 기능

        perfectTick = perfectTime / 1000f * (bpm / 60f) * resolution;
        goodTick = goodTime / 1000f * (bpm / 60f) * resolution;
        missTick = missTime / 1000f * (bpm / 60f) * resolution;
        judgeSystem.Init(perfectTick, goodTick, missTick, lane); // JudgeSystem 초기화, perfectTick, goodTick, missTick, lane 전달

        int noteNumber = noteManager.getNoteNumber();
        uiManager.Init(noteNumber, chartData.metaData); // UIManager 초기화
        Debug.Log($"[GameManager] noteNumber: {noteNumber}");

        playerController.Init();

        resultManager.Init(noteNumber);
        resultManager.SetTitle(chartData.metaData.title);
        resultManager.SetProgress(progress);

        Debug.Log($"[GameManager] arriveTime: {arriveTime}, startDelay: {startDelay}");
        StartCoroutine(ShowReadyUI());
    }

    public void ChangeBpm(float bpm)
    {
        float previousBpm = this.bpm;
        this.bpm = bpm;
        noteSpeed = noteSpeed * (bpm / previousBpm); // 노트 스피드 조정
        TickClock.Instance.ChangeBpm(bpm); // TickClock에 BPM 변경 알림
        arriveTime = laneController.getArriveTime() + startDelay; // LaneController로부터 arriveTime 재계산
        arriveTick = arriveTime * (bpm / 60f) * resolution;
        noteManager.ChangeBpm(noteSpeed, arriveTick); // NoteSpawner에 BPM 변경 알림, noteSpeed와 arriveTick 전달
        beatLineManager.ChangeBpm(noteSpeed, arriveTick);
    }

    public void OnMusicEnd()
    {
        resultManager.UpdateMaxCombo(uiManager.combo);
        resultManager.SetScore(uiManager.score);
        ResultData resultData = resultManager.GetResultData();
        Debug.Log($"[GameManager] Result Data perfectCount : {resultData.perfectCount} , goodCount : {resultData.goodCount} ,missCount : {resultData.missCount} ,score : {resultData.score} ,grade : {resultData.grade} ,maxCombo : {resultData.maxCombo} ,isFullCombo : {resultData.isFullCombo} ,isAllPerfect : {resultData.isAllPerfect} ,progress : {resultData.progress} ,progressChange : {resultData.progressChange}");
        LoadResultScene(resultData);
    }

    private void LoadResultScene(ResultData resultData)
    {
        DataCarrier.Instance.SetData(resultData);
        SceneManager.LoadScene("ResultScene");
    }
    private IEnumerator ShowReadyUI()
    {
        audioSource.Play(); // Ready 사운드 재생
        readyUIObject.SetActive(true);
        yield return new WaitForSeconds(startDelay);
        readyUIObject.SetActive(false);
    }
}
