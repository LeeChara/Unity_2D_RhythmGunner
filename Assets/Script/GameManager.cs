using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LaneController laneController;
    public NoteSpawner noteSpawner;
    public JudgeSystem judgeSystem;
    public MusicPlayer musicPlayer;

    public Transform lane;

    public float perfectTime = 75.0f; // ms
    public float goodTime = 150.0f; // ms
    public float missTime = 250.0f; // ms
    public float noteSpeed = 100.0f;

    public float perfectTick { get; private set; }
    public float goodTick { get; private set; }
    public float missTick { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        float bpm = 75f;
        int resolution = 480;
        Debug.Log("GameManager Start: bpm = " + bpm + ", resolution = " + resolution);
        float audioOffset = 0f; // æÁºˆ: ¿Ωæ«¿Ã ¥ ∞‘ Ω√¿€, ¿Ωºˆ: ¿Ωæ«¿Ã ¿œ¬Ô Ω√¿€
        laneController.Init(noteSpeed);
        float arriveTime = laneController.getArriveTime();
        TickClock.Instance.Init(bpm, resolution, arriveTime);
        float arriveTick = arriveTime * (bpm / 60f) * resolution;
        noteSpawner.Init(resolution, noteSpeed, arriveTick);
        musicPlayer.Init(arriveTime, audioOffset);

        perfectTick = perfectTime / 1000f * (bpm / 60f) * resolution;
        goodTick = goodTime / 1000f * (bpm / 60f) * resolution;
        missTick = missTime / 1000f * (bpm / 60f) * resolution;

        judgeSystem.Init(perfectTick, goodTick, missTick, lane);
    }

}
