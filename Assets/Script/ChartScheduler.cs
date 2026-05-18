using UnityEngine;
using System.Collections.Generic;

public class ChartScheduler : MonoBehaviour
{
    private List<EventData> events;

    public NoteSpawner noteSpawner;
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
            }
        }
    }
}
