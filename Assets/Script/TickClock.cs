using UnityEngine;

public class TickClock : MonoBehaviour
{
    public static TickClock Instance { get; private set; }
    public double Tick { get; private set; }
    public float Bpm { get; private set; }
    public int Resolution { get; private set; }

    public double SongStartDspTime { get; private set; }
    public void SetSongStartDspTime (double startDspTime)
    {
        this.SongStartDspTime = startDspTime;
    }
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        Tick = (AudioSettings.dspTime - this.SongStartDspTime) * (Bpm / 60f) * Resolution;
    }

    public void Init(float bpm, int resolution, float arriveTime)
    {
        this.Bpm = bpm;
        this.Resolution = resolution;
    }

    public void ChangeBpm(float bpm)
    {
        this.Bpm = bpm;
    }
}
