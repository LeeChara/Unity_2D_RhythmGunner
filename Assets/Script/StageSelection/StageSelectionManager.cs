using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StageSelectionManager : MonoBehaviour
{
    public static StageSelectionManager Instance;

    [Header("Scene")]

#if UNITY_EDITOR
    [SerializeField]
    private SceneAsset mainMenuScene;
#endif

    [SerializeField, HideInInspector]
    private string mainMenuSceneName;

    [Header("Preview")]
    public GameObject previewPrefab;
    public Transform preview;

    [Header("Stage Button")]
    public GameObject SongButtons;

    [Header("BGM")]
    public AudioSource bgmAudioSource;

    [Header("Fade")]
    public CanvasGroup fadeCanvasGroup;

    [Header("SE")]
    public AudioSource seAudioSource;
    public AudioClip backSE;

    private GameObject currentPreview;
    private bool isLoading;

    private void Awake()
    {
        Instance = this;

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = false;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (mainMenuScene != null)
        {
            mainMenuSceneName = mainMenuScene.name;
        }
    }
#endif

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isLoading)
                return;

            if (currentPreview != null)
            {
                ClosePreview();
            }
            else
            {
                StartCoroutine(ReturnToMainMenu());
            }
        }
    }

    private IEnumerator ReturnToMainMenu()
    {
        isLoading = true;

        if (backSE != null &&
            seAudioSource != null)
        {
            seAudioSource.PlayOneShot(backSE);
        }

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.blocksRaycasts = true;

            float timer = 0f;
            float fadeDuration = 0.4f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                fadeCanvasGroup.alpha =
                    Mathf.Lerp(
                        0f,
                        1f,
                        timer / fadeDuration);

                yield return null;
            }

            fadeCanvasGroup.alpha = 1f;
        }

        if (backSE != null)
        {
            yield return new WaitForSeconds(backSE.length);
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OpenSongPreview(SongData songData)
    {
        SongButtons.SetActive(false);

        if (bgmAudioSource != null)
        {
            bgmAudioSource.Pause();
        }

        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        currentPreview =
            Instantiate(previewPrefab, preview);

        SongPreview ui =
            currentPreview.GetComponent<SongPreview>();

        ui.Setup(songData);
    }

    public void ClosePreview()
    {
        SongButtons.SetActive(true);

        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }

        if (bgmAudioSource != null)
        {
            bgmAudioSource.UnPause();
        }
    }
}