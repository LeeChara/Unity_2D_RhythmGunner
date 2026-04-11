using UnityEngine;
using UnityEngine.Audio;

// Conductor.cs
// Calculates current tick
// Plays audio
public class Conductor : MonoBehaviour
{
    //싱글톤 패턴을 적용하여 Conductor 인스턴스를 전역에서 접근할 수 있도록 합니다.
    public static Conductor Instance { get; private set; }

    public BeatSpawner beatSpawner;

    AudioSource audioSource;
    public float currentTick = 0;
    public float bpm = 10;
    public int resolution = 10;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        if (beatSpawner == null)
        {
            Debug.LogError("Conductor: BeatSpawner reference is not set!");
        }
    }

    private void Update()
    {
        currentTick = audioSource.time * (bpm / 60f) * resolution;
    }

    // 초기화 메서드입니다. ChartData에서 MetaData를 받아와서 bpm, resolution을 설정하고, 음악을 재생합니다.
    public void Init(MetaData metaData)
    {
        this.bpm = metaData.bpm;
        this.resolution = metaData.resolution;
        audioSource.resource = Resources.Load<AudioResource>($"Music/{metaData.title}");
        audioSource.Play();
        Debug.Log($"Conductor: Playing {metaData.title} with BPM {bpm} and resolution {resolution}");

        beatSpawner.Init();
    } 
}
