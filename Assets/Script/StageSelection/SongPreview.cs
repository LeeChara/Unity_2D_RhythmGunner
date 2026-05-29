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

    public TMP_Text easyText;
    public TMP_Text normalText;
    public TMP_Text hardText;

    [Header("Audio")]
    public AudioSource audioSource;

    private SongData currentSong;

    public void Setup(SongData songData)
    {
        currentSong = songData;

        jacketImage.sprite = songData.jacketImage;

        songNameText.text = songData.songTitle;
        artistText.text = songData.artist;
        descriptionText.text = songData.description;

        easyText.text = $"EASY  {songData.easyLevel}";
        normalText.text = $"NORMAL  {songData.normalLevel}";
        hardText.text = $"HARD  {songData.hardLevel}";

        PlayPreview(songData.previewClip);
    }

    void PlayPreview(AudioClip clip)
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
        if (currentSong == null)
            return;

        SceneManager.LoadScene(currentSong.playSceneName);
    }

    public void OnClickClose()
    {
        audioSource.Stop();

        StageSelectionManager.Instance.ClosePreview();
    }
}