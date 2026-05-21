using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ChartData를 받아서 이벤트를 스케줄링하는 클래스. 각 이벤트 담당 클래스에게 이벤트를 전달
/// </summary>
public class ChartScheduler : MonoBehaviour
{
    private List<EventData> events;

    public NoteSpawner noteSpawner;
    public EnemySpawner enemySpawner;
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
                    noteSpawner.AddSchedule(e as NoteData);
                    break;
                case "Enemy":
                    enemySpawner.AddSchedule(e as EnemyData);
                    break;

            }
        }
    }
}
