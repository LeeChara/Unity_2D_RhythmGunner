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
        double songStartDspTime = AudioSettings.dspTime + arriveTime + audioOffset;
        audioSource.PlayScheduled(songStartDspTime);
        TickClock.Instance.SetSongStartDspTime(songStartDspTime); // Music Start Tick is 0

        Debug.Log($"[MusicPlayer] Music scheduled to play at time: {songStartDspTime - AudioSettings.dspTime}, dspTime: {AudioSettings.dspTime}, songStartDspTime: {songStartDspTime}");
    }
}
