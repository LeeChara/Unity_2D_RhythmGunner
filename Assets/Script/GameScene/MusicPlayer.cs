using UnityEngine;
public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (audioSource.time >= audioSource.clip.length - 0.1f)
        {
            Debug.Log("[MusicPlayer] Music Ends");
            GameManager.Instance.OnMusicEnd();
        }
    }
    public void Init(string musicFileName, float arriveTime, float audioOffset)
    {
        Debug.Log($"[MusicPlayer] SongName : {musicFileName}");
        AudioClip clip = Resources.Load<AudioClip>("Music/" + musicFileName);
        audioSource.resource = clip;
        audioSource.clip = clip;
        double songStartDspTime = AudioSettings.dspTime + arriveTime + audioOffset;
        double calibratedDspTime = songStartDspTime + SettingManager.Instance.NoteSpawnOffset / 1000;
        audioSource.PlayScheduled(calibratedDspTime);
        TickClock.Instance.SetSongStartDspTime(songStartDspTime);
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
    public float ClipLength => audioSource.clip != null ? audioSource.clip.length : 0f;
}