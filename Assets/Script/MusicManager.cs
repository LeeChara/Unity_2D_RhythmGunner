using UnityEngine;
using UnityEngine.InputSystem;

public class MusicManager : MonoBehaviour
{
    GameObject musicPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicPlayer = GameObject.Find("MusicPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (musicPlayer.GetComponent<AudioSource>().isPlaying)
            {
                musicPlayer.GetComponent<AudioSource>().Pause();
            }
            else
            {
                musicPlayer.GetComponent<AudioSource>().Play();
            }
        }
    }
}
