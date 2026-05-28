using TMPro;
using UnityEngine;

/// <summary>
/// 테스트용 클래스 - TickClock의 Tick 값을 화면에 표시하고, 음악이 시작될 때 로그를 출력. 삭제 예정
/// </summary>
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
