using UnityEngine;

public class StageSelectionManager : MonoBehaviour
{
    public static StageSelectionManager Instance;

    [Header("Preview")]
    public GameObject previewPrefab;

    public Transform preview;

    [Header("Stage Button")]
    public GameObject SongButtons;

    private GameObject currentPreview;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenSongPreview(SongData songData)
    {
        SongButtons.SetActive(false);
        
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
        }
    }
}
