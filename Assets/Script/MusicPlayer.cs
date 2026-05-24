using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Init(float arriveTime, float audioOffset)
    {
        // DSP 시간은 오디오 시스템에서 사용하는 시간으로, 음악이 정확한 타이밍에 재생되도록 하기 위해 사용
        // 게임의 모든 요소는 음악을 기준으로 동기화함
        double songStartDspTime = AudioSettings.dspTime + arriveTime + audioOffset; // 음악이 시작될 DSP 시간 계산, 현재 DSP 시간에서 도착 시간과 오프셋을 더해서 계산
        audioSource.PlayScheduled(songStartDspTime);
        TickClock.Instance.SetSongStartDspTime(songStartDspTime); // 음악이 시작된 시점의 DSP 시간을 TickClock에 설정, 음악은 항상 Tick 0에서 시작하도록

        Debug.Log($"[MusicPlayer] Music scheduled to play at time: {songStartDspTime - AudioSettings.dspTime}, dspTime: {AudioSettings.dspTime}, songStartDspTime: {songStartDspTime}");
    }

    public void JumpTo(float jumpTime)
    {
        audioSource.Stop();
        audioSource.time = jumpTime;
        double songStartDspTime = AudioSettings.dspTime - jumpTime;
        TickClock.Instance.SetSongStartDspTime(songStartDspTime);
        audioSource.Play();
    }
}
