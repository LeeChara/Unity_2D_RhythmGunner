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
        audioSource.PlayScheduled(AudioSettings.dspTime + arriveTime + audioOffset);
        Debug.Log($"MusicPlayer scheduled to play at: {AudioSettings.dspTime + arriveTime + audioOffset}");
    }
}
