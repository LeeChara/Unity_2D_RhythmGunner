using UnityEngine;
using System.Collections.Generic;

public class AlertEffectManager : MonoBehaviour
{
    public AlertEffectController alertEffectController;
    private List<AlertEffectData> alertEffects = new List<AlertEffectData>();

    void Update()
    {
        while (alertEffects.Count > 0 && TickClock.Instance.Tick >= alertEffects[0].tick)
        {
            alertEffectController.Alert(alertEffects[0].duration);
            alertEffects.RemoveAt(0);
        }
    }

    public void Init()
    {
        alertEffectController.Init();
    }

    public void AddSchedule(AlertEffectData alertEffectData)
    {
        alertEffects.Add(alertEffectData);
    }

    public void SkipEvent(float jumpTick)
    {
        for (int i = alertEffects.Count - 1; i >= 0; i--)
        {
            if (alertEffects[i].tick <= jumpTick)
            {
                alertEffects.RemoveAt(i);
            }
        }
    }
}
