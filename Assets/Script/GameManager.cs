using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LaneController laneController;
    public NoteSpawner noteSpawner;
    public JudgeSystem judgeSystem;

    public float perfectTime = 23.0f, goodTime = 80.0f; // ms
    public float noteSpeed = 5f;

    public float perfectTick { get; private set; }
    public float goodTick { get; private set; }
    void Start()
    {
        float bpm = 120f;
        int resolution = 480;
        laneController.Init(noteSpeed);
        float arriveTime = laneController.getArriveTime();
        TickClock.Instance.Init(bpm, resolution, arriveTime);
        float arriveTick = arriveTime * (bpm / 60f) * resolution;
        noteSpawner.Init(resolution, noteSpeed, arriveTick);

        perfectTick = perfectTime / 1000f * (bpm / 60f) * resolution;
        goodTick = goodTime / 1000f * (bpm / 60f) * resolution;

        judgeSystem.Init(perfectTick, goodTick);
    }

}
