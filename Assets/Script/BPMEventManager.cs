using UnityEngine;
using System.Collections.Generic;

public class BPMEventManager : MonoBehaviour
{
    private List<BPMEventData> bpmEvents = new List<BPMEventData>();
    void Update()
    {
        while (bpmEvents.Count > 0 && TickClock.Instance.Tick >= bpmEvents[0].tick)
        {
            GameManager.Instance.ChangeBpm(bpmEvents[0].bpm);
            bpmEvents.RemoveAt(0);
        }
    }

    // 스케줄 추가 메서드. ChartScheduler에서 호출됨
    public void AddSchedule(BPMEventData bpmEventData)
    {
        // Debug.Log($"[EventManager] Scheduled BPM Event - Tick: {scheduledBPMEventData.tick}, BPM: {scheduledBPMEventData.bpm}");
        bpmEvents.Add(bpmEventData);
    }
}
