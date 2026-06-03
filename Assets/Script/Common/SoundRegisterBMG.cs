using UnityEngine;
using UnityEngine.Audio;

public class SoundRegisterBmg : MonoBehaviour
{
    public AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        SettingManager.Instance.RegisterBgm(audioSource);
    }

    private void OnDestroy()
    {
        SettingManager.Instance.UnregisterBgm(audioSource);
    }
}
