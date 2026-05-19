using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public JSONConverter jsonConverter;
    public ChartDataViewer chartDataViewer;
    public ChartScheduler chartScheduler;

    public LaneController laneController;
    public JudgeSystem judgeSystem;
    public MusicPlayer musicPlayer;
    public NoteSpawner noteSpawner;
    public EnemySpawner enemySpawner;

    public Transform lane;

    public float perfectTime; // ms
    public float goodTime; // ms
    public float missTime; // ms
    public float noteSpeed; // Test value

    public float perfectTick { get; private set; }
    public float goodTick { get; private set; }
    public float missTick { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // Test Code
        ChartData chartData = jsonConverter.Load("Test");
        //Debug.Log("[GameManager]ChartData loaded: " + chartData.metaData.title);
        // chartDataViewer.ViewChartData(chartData);

        float bpm = chartData.metaData.bpm;
        int resolution = chartData.metaData.resolution;
        float audioOffset = chartData.metaData.offset; // sec, positive: delay music, negative: advance music
        //Debug.Log("[GameManager] Start: bpm = " + bpm + ", resolution = " + resolution + ", offset = " + audioOffset);

        laneController.Init(noteSpeed);
        float arriveTime = laneController.getArriveTime();

        musicPlayer.Init(arriveTime, audioOffset);
        TickClock.Instance.Init(bpm, resolution, arriveTime);
        Debug.Log($"[GameManager] arriveTime: {arriveTime}, audioOffset: {audioOffset}");

        float arriveTick = arriveTime * (bpm / 60f) * resolution;
        noteSpawner.Init(resolution, noteSpeed, arriveTick, laneController.moveDistance);
        enemySpawner.Init();

        chartScheduler.Init(chartData);

        perfectTick = perfectTime / 1000f * (bpm / 60f) * resolution;
        goodTick = goodTime / 1000f * (bpm / 60f) * resolution;
        missTick = missTime / 1000f * (bpm / 60f) * resolution;
        judgeSystem.Init(perfectTick, goodTick, missTick, lane);
    }
}
