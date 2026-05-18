using UnityEngine;

public class TickClock : MonoBehaviour
{
    public static TickClock Instance { get; private set; }
    public float Tick { get; private set; } = 0f;

    private float bpm;
    private int resolution;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        Tick += Time.deltaTime * (bpm / 60f) * resolution;
    }

    public void Init(float bpm, int resolution, float arriveTime)
    {
        this.bpm = bpm;
        this.resolution = resolution;
        Tick = - arriveTime * (bpm / 60f) * resolution;
        Debug.Log($"[TickClock] Initialized: initial Tick = {Tick}");
    }

    public void ChangeBpm(float bpm)
    {
        this.bpm = bpm;
    }
}
