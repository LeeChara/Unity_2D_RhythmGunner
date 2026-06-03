using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSE;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            audioSource.PlayOneShot(clickSE);
        }
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
