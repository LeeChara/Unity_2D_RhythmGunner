using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    bool hasPlayed = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(TickClock.Instance.Tick >= 0f && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
