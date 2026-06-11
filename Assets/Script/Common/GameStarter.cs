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
            songName = "We Could Get More Machinegun Psystyle!_Hard";
        }

        Debug.Log($"[GameStarter] Selected Song : {songName}");
        GameStart();
    }

    public void GameStart()
    {
        Debug.Log($"[GameStarter] SongName : {songName}");
        GameManager.Instance.Init(songName);
    }
}