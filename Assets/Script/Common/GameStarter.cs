using Newtonsoft.Json;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public static GameStarter Instance { get; private set; }

    public string songName;

    public bool isAuto;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (songName == null) songName = "Test";

        Invoke("GameStart", 3.0f);
    }

    private void GameStart()
    {
        Debug.Log($"[GameStarter] SongName : {songName}");
        GameManager.Instance.Init(songName);
    }
}