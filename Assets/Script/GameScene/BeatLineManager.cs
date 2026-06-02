using UnityEngine;
using System.Collections.Generic;

public class BeatLineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BeatLinePrefab;
    public RectTransform lane;
    private float arriveTick;
    private float destroyX;
    private float moveDistance;
    private float noteSpeed;
    private List<float> beatLines = new List<float>();
    private bool isInitialized = false;

    private void Update()
    {
        if (!isInitialized) return;
        while (beatLines.Count > 0 && TickClock.Instance.Tick >= beatLines[0])
        {
            SpawnBeatLine(beatLines[0]);
            beatLines.RemoveAt(0);
        }
    }

    public void Init(int resolution, float noteSpeed, float arriveTick, float moveDistance, float startTick, float endTick)
    {
        this.noteSpeed = noteSpeed;
        this.arriveTick = arriveTick;
        this.moveDistance = moveDistance;
        this.destroyX = lane.rect.width;

        for (float tick = startTick; tick <= endTick; tick += resolution * 2)
        {
            AddSchedule(tick);
        }

        isInitialized = true;
    }

    private void AddSchedule(float targetTick)
    {
        beatLines.Add(targetTick - arriveTick);
    }

    private void SpawnBeatLine(float spawnTick)
    {
        GameObject obj = Instantiate(BeatLinePrefab);
        obj.GetComponent<RectTransform>().SetParent(lane, false);
        obj.GetComponent<BeatLineController>().Init(spawnTick + arriveTick, moveDistance, destroyX);
    }

    public void ChangeBpm(float noteSpeed, float arriveTick)
    {
        this.noteSpeed = noteSpeed;
        this.arriveTick = arriveTick;
    }
}