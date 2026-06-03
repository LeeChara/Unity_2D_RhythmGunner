using UnityEngine;
using UnityEngine.Audio;

public class SoundRegisterSe : MonoBehaviour
{
    public AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        SettingManager.Instance.RegisterSe(audioSource);
    }

    private void OnDestroy()
    {
        SettingManager.Instance.UnregisterSe(audioSource);
    }
}
