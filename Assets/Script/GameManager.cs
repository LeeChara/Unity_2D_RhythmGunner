using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LaneController laneController;
    public NoteSpawner noteSpawner;
    public JudgeSystem judgeSystem;

    public float perfectTime = 23.0f, goodTime = 80.0f; // ms
    public float noteSpeed = 5f;

    public float perfectTick { get; private set; } //변수 읽기 모드. 외부에서 참조는 가능하지만 수정 불가.
    public float goodTick { get; private set; } //변수 읽기 모드. 외부에서 참조는 가능하지만 수정 불가.
    void Start()
    {
        float bpm = 120f;
        int resolution = 480; //이건 무슨 변수인가요?
        laneController.Init(noteSpeed);
        float arriveTime = laneController.getArriveTime();
        TickClock.Instance.Init(bpm, resolution, arriveTime);
        float arriveTick = arriveTime * (bpm / 60f) * resolution; //노트가 판정선에 도달하는 시간(초)을 틱으로 변환. ms를 그대로 쓰면 싱크가 안 맞음.
        noteSpawner.Init(resolution, noteSpeed, arriveTick);

        perfectTick = perfectTime / 1000f * (bpm / 60f) * resolution; //퍼팩트 타임을 틱으로 변환. 틱으로 변환하는 이유는 위와 같음
        goodTick = goodTime / 1000f * (bpm / 60f) * resolution; //굿 타임을 틱으로 변환. 틱으로 변환하는 이유는 위와 같음

        judgeSystem.Init(perfectTick, goodTick);
    }

}
