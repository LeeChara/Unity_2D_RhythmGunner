using TMPro;
using UnityEngine;

public class TickTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tickText;

    bool logAvailable = true;
    void Update()
    {
        bool isPlaying = AudioSettings.dspTime >= TickClock.Instance.SongStartDspTime;
        tickText.text = $"Tick: {TickClock.Instance.Tick:F0}\n{(isPlaying ? "Playing" : "Waiting")}";

        if (logAvailable && isPlaying)
        {
            Debug.Log($"[TickTest] Music Played at Tick: {TickClock.Instance.Tick:F0}");
            logAvailable = false;
        }
    }
}
