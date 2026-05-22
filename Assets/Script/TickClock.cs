using UnityEngine;

/// <summary>
/// Tick 계산 클래스. 싱글톤 패턴으로 구현되어 다른 클래스에서 쉽게 접근 가능
/// </summary>
public class TickClock : MonoBehaviour
{
    public static TickClock Instance { get; private set; }
    public double Tick { get; private set; }
    public float Bpm { get; private set; }
    public int Resolution { get; private set; }

    public double SongStartDspTime { get; private set; }
    public void SetSongStartDspTime (double startDspTime) // 음악이 시작된 시점의 DSP 시간을 설정하는 메서드
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
