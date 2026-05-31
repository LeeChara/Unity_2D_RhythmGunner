using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SongPreview : MonoBehaviour
{
    [Header("UI")]
    public Image jacketImage;

    public TMP_Text songNameText;
    public TMP_Text artistText;
    public TMP_Text bpmText;
    public TMP_Text descriptionText;

    [Header("Difficulty")]
    [SerializeField]
    private Difficulty selectedDifficulty =
        Difficulty.Normal;

    public Difficulty CurrentDifficulty
    {
        get { return selectedDifficulty; }
    }

    [Header("Audio")]
    public AudioSource audioSource;

    private SongData currentSong;

    public void Setup(SongData songData) // 곡 정보
    {
        currentSong = songData;

        jacketImage.sprite = songData.jacketImage;

        songNameText.text = songData.songTitle;
        bpmText.text = $"BPM: {songData.BPM}";
        artistText.text = songData.artist;
        descriptionText.text = songData.description;

        PlayPreview(songData.previewClip);

        RefreshDifficultyButtons();
    }

    public void SelectDifficulty(Difficulty difficulty) // 선택된 난이도
    {
        selectedDifficulty = difficulty;

        RefreshDifficultyButtons();
    }

    private void RefreshDifficultyButtons()
    {
        DifficultyButton[] buttons =
            GetComponentsInChildren<DifficultyButton>();

        foreach (DifficultyButton button in buttons)
        {
            button.UpdateVisual();
        }
    }

    private void PlayPreview(AudioClip clip) // 곡 미리 듣기
    {
        if (clip == null)
            return;

        audioSource.Stop();

        audioSource.clip = clip;
        audioSource.volume = 1f;

        audioSource.Play();
    }

    public void OnClickPlay() // PLAY 버튼 클릭
    {
        if (currentSong == null)
            return;

        GameData.SelectedSong =
            currentSong;

        GameData.SelectedDifficulty =
            selectedDifficulty;

        SceneManager.LoadScene(
            currentSong.playSceneName);
    }

    public void OnClickClose() // CLOSE 버튼 클릭
    {
        audioSource.Stop();

        StageSelectionManager.Instance.ClosePreview();
    }
}