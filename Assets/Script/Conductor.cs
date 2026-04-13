using UnityEngine;
using UnityEngine.Audio;

// Conductor.cs
// Calculates current tick
// Plays audio
public class Conductor : MonoBehaviour
{
    //싱글톤 패턴을 적용하여 Conductor 인스턴스를 전역에서 접근할 수 있도록 합니다.
    public static Conductor Instance { get; private set; }

    public AudioSource audioSource;
    public BeatSpawner beatSpawner;
    public NoteSpawner noteSpawner;
    public NoteTranslator noteTranslator;

    public float currentTick = 0;
    public float bpm = 10;
    public int resolution = 10;
    public float delayTime = 5f;
    public float arriveTiime = 100f;
    public float speed = 5.0f;
    public float noteSpeed = 0.1f;
    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        beatSpawner = GetComponent<BeatSpawner>();
        noteSpawner = GetComponent<NoteSpawner>();
        noteTranslator = GetComponent<NoteTranslator>();
    }

    private void Update()
    {
        currentTick += Time.deltaTime * (bpm / 60f) * resolution;
        //Debug.Log($"Conductor: Current Tick = {currentTick}");
    }

    // 초기화 메서드입니다. ChartData에서 MetaData를 받아와서 bpm, resolution을 설정하고, 음악을 재생합니다.
    public void Init(MetaData metaData)
    {
        this.bpm = metaData.bpm;
        this.resolution = metaData.resolution;
        audioSource.resource = Resources.Load<AudioResource>($"Music/{metaData.title}");
        Debug.Log($"Conductor: PlayDelayed {delayTime}초 후 재생 시작");
        audioSource.PlayDelayed(delayTime);
        Debug.Log($"Conductor: Playing {metaData.title} with BPM {bpm} and resolution {resolution}");

        currentTick = - delayTime * resolution * (bpm / 60f); // 음악이 시작하기 전에 일정 시간 동안 음표가 스폰될 수 있도록 초기 tick을 설정합니다.
        noteSpeed = speed * bpm / 2f; // 노트의 속도를 계산합니다. bpm과 speed에 비례합니다.

        beatSpawner.Init();
    } 

}
