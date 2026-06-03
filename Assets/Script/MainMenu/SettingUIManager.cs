using UnityEngine;
using UnityEngine.UI;

public class SettingUIManager : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;
    [SerializeField] private Slider judgementSlider;
    [SerializeField] private Slider musicDelaySlider;

    private void Start()
    {
        // 열릴 때 현재 저장된 값으로 슬라이더 초기화
        bgmSlider.value = SettingManager.Instance.BgmVolume;
        seSlider.value = SettingManager.Instance.SeVolume;
        judgementSlider.value = SettingManager.Instance.JudgementOffset;
        musicDelaySlider.value = SettingManager.Instance.NoteSpawnOffset;
    }

    public void OnBgmChanged(float value)
    {
        SettingManager.Instance.SetBgmVolume(value);
    }

    public void OnSeChanged(float value)
    {
        SettingManager.Instance.SetSeVolume(value);
    }

    public void OnJudgementChanged(float value)
    {
        SettingManager.Instance.SetJudgementOffset(value);
    }

    public void OnMusicDelayChanged(float value)
    {
        SettingManager.Instance.SetNoteSpawnOffset(value);
    }
}