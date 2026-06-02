using UnityEngine;

public class StageButton : MonoBehaviour
{
    [Header("Song")]
    public SongData songData;

    public void OnClickArea()
    {
        StageSelectionManager.Instance.OpenSongPreview(songData);
    }
}