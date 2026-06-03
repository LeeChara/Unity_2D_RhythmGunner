using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSE;
    private Vector3 originalScale;
    private void Awake()
    {
        originalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Entered");
        textObject.SetActive(true);
        transform.localScale = originalScale * 1.1f;
        audioSource.PlayOneShot(hoverSE);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textObject.SetActive(false);
        transform.localScale = originalScale;
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
