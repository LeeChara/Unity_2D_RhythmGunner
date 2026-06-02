using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SongPreview : MonoBehaviour
{
    [Header("Difficulty Buttons")]
    public DifficultyButton[] difficultyButtons;

    [Header("UI")]
    public Image jacketImage;

    public TMP_Text songNameText;
    public TMP_Text artistText;
    public TMP_Text bpmText;
    public TMP_Text descriptionText;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("SE")]
    public AudioSource seAudioSource;
    public AudioClip playSE;

    [Header("Difficulty")]
    [SerializeField]
    private Difficulty selectedDifficulty =
        Difficulty.Normal;

    private SongData currentSong;

    private bool isLoading = false;

    public Difficulty CurrentDifficulty
    {
        get { return selectedDifficulty; }
    }

    private void Awake()
    {
        difficultyButtons =
            GetComponentsInChildren<DifficultyButton>(true);
    }

    public void Setup(SongData songData)
    {
        currentSong = songData;

        jacketImage.sprite = songData.jacketImage;

        songNameText.text = songData.songTitle;
        artistText.text = songData.artist;
        bpmText.text = songData.BPM.ToString();
        descriptionText.text = songData.description;

        PlayPreview(songData.previewClip);

        RefreshDifficultyButtons();
    }

    public void SelectDifficulty(Difficulty difficulty)
    {
        selectedDifficulty = difficulty;

        RefreshDifficultyButtons();
    }

    private void RefreshDifficultyButtons()
    {
        foreach (DifficultyButton button in difficultyButtons)
        {
            button.UpdateVisual();
        }
    }

    private void PlayPreview(AudioClip clip)
    {
        if (clip == null)
            return;

        audioSource.Stop();

        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void OnClickPlay()
    {
        if (isLoading)
            return;

        StartCoroutine(PlayAndLoadScene());
    }

    private IEnumerator PlayAndLoadScene()
    {
        isLoading = true;

        GameData.SelectedSong =
            currentSong;

        GameData.SelectedDifficulty =
            selectedDifficulty;

        CanvasGroup fade =
            StageSelectionManager.Instance.fadeCanvasGroup;

        // Preview 음악 정지
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // PLAY SE 재생
        if (playSE != null &&
            seAudioSource != null)
        {
            seAudioSource.PlayOneShot(playSE);
        }

        // FadePanel 활성화
        if (fade != null)
        {
            fade.gameObject.SetActive(true);
            fade.blocksRaycasts = true;
        }

        float fadeDuration = 0.5f;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            if (fade != null)
            {
                fade.alpha =
                    Mathf.Lerp(
                        0f,
                        1f,
                        timer / fadeDuration);
            }

            yield return null;
        }

        if (fade != null)
        {
            fade.alpha = 1f;
        }

        // SE 끝날 때까지 대기
        if (playSE != null)
        {
            yield return new WaitForSeconds(
                playSE.length);
        }

        SceneManager.LoadScene(
            currentSong.playSceneName);
    }

    public void OnClickClose()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        StageSelectionManager.Instance.ClosePreview();
    }
}