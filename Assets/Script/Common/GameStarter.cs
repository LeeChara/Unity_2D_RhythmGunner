using Newtonsoft.Json;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public static GameStarter Instance { get; private set; }

    public string songName;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameData.SelectedSong != null)
        {
            string difficulty = GameData.SelectedDifficulty.ToString();
            songName = GameData.SelectedSong.songTitle + "_" + difficulty;
        }
        else
        {
            songName = "Hidden(秘伝)_Easy";
        }

        Debug.Log($"[GameStarter] Selected Song : {songName}");
        GameStart();
    }

    private void GameStart()
    {
        Debug.Log($"[GameStarter] SongName : {songName}");
        GameManager.Instance.Init(songName);
    }
}