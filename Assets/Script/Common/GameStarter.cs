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
        Debug.Log($"[GameStarter] SongName : {songName}");
        GameManager.Instance.Init(songName);
    }
}