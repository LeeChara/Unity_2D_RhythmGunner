using UnityEngine;

public class TickClock : MonoBehaviour
{
    public static TickClock Instance { get; private set; }
    public double Tick { get; private set; }
    public float Bpm { get; private set; }
    public int Resolution { get; private set; }
    public double previousDspTime;
    public double previousTick;
    public double SongStartDspTime { get; private set; }

    public void SetSongStartDspTime(double startDspTime)
    {
        this.SongStartDspTime = startDspTime;
    }
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (PauseManager.Instance.IsPaused) return;
        Tick = previousTick + (AudioSettings.dspTime - previousDspTime) * (Bpm / 60f) * Resolution;
    }
    public void Init(float bpm, int resolution)
    {
        this.Bpm = bpm;
        this.Resolution = resolution;
        previousTick = 0;
        previousDspTime = SongStartDspTime;
    }
    public void ChangeBpm(float bpm)
    {
        previousTick = Tick;
        previousDspTime = AudioSettings.dspTime;
        this.Bpm = bpm;
        Debug.Log($"[TickClock] BPM changed to: {bpm}, previousTick: {previousTick}, previousDspTime: {previousDspTime}");
    }
    public void JumpTo(float previousTick, float bpm)
    {
        this.previousTick = previousTick;
        previousDspTime = AudioSettings.dspTime;
        this.Bpm = bpm;
    }
    public void OnPause()
    {
        previousTick = Tick;
    }
    public void OnResume()
    {
        previousDspTime = AudioSettings.dspTime;
    }
}