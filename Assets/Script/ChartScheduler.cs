using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ChartData를 받아서 이벤트를 스케줄링하는 클래스. 각 이벤트 담당 클래스에게 이벤트를 전달
/// </summary>
public class ChartScheduler : MonoBehaviour
{
    private List<EventData> events;

    public NoteManager noteManager;
    public EnemyManager enemyManager;
    public BossManager bossManager;
    public TextEffectManager textEffectManager;
    public NoteEffectManager noteEffectManager;
    public BPMEventManager bpmEventManager;
    public void Init(ChartData chartData)
    {
        this.events = chartData.events;

        ScheduleEvent();
    }

    private void ScheduleEvent()
    {
        foreach (EventData e in events)
        {
            switch (e.type)
            {
                case "Note":
                    noteManager.AddSchedule(e as NoteData);
                    break;
                case "Enemy":
                    enemyManager.AddSchedule(e as EnemyData);
                    break;
                case "Boss":
                    bossManager.AddSchedule(e as BossData);
                    break;
                case "TextEffect":
                    textEffectManager.AddSchedule(e as TextEffectData);
                    break;
                case "NoteEffect":
                    noteEffectManager.AddSchedule(e as NoteEffectData);
                    break;
                case "BPMEvent":
                    bpmEventManager.AddSchedule(e as BPMEventData);
                    break;
            }
        }
    }
}
