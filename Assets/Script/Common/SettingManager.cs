using UnityEngine;
using System.Collections.Generic;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }
    public float JudgementOffset; // tick 단위, 판정 타이밍 조절, 양수: 일찍 눌러야함, 음수: 늦게 눌러야함
    public float NoteSpawnOffset; // ms 단위, 노트 소환 타이밍 조절, 양수: 노트가 빨리 소환, 음수: 노트가 늦게 소환 
    public float BgmVolume;
    public float SeVolume;
    public bool IsAuto { get; private set; }

    public bool ActivateJump;
    public float JumpTick;
    public void SetIsAuto(bool isAuto)
    {
        Debug.Log($"[SettingManager] Set IsAuto to {isAuto}");
        IsAuto = isAuto;
    }

    private List<AudioSource> bgmSources = new List<AudioSource>();
    private List<AudioSource> seSources = new List<AudioSource>();
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void SetJudgementOffset(float offset)
    {
        JudgementOffset = offset;
        Save();
    }
    public void SetNoteSpawnOffset(float offset)
    {
        NoteSpawnOffset = offset;
        Save();
    }
    public void SetBgmVolume(float volume)
    {
        BgmVolume = volume;
        foreach (AudioSource source in bgmSources)
            source.volume = volume;
        Save();
    }

    public void SetSeVolume(float volume)
    {
        SeVolume = volume;
        foreach (AudioSource source in seSources)
            source.volume = volume;
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("JudgementOffset", JudgementOffset);
        PlayerPrefs.SetFloat("NoteSpawnOffset", NoteSpawnOffset);
        PlayerPrefs.SetFloat("BgmVolume", BgmVolume);
        PlayerPrefs.SetFloat("SeVolume", SeVolume);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        JudgementOffset = PlayerPrefs.GetFloat("JudgementOffset", 0f);
        NoteSpawnOffset = PlayerPrefs.GetFloat("NoteSpawnOffset", 0f);
        BgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1f);
        SeVolume = PlayerPrefs.GetFloat("SeVolume", 1f);
    }
    public void RegisterBgm(AudioSource source)
    {
        if (!bgmSources.Contains(source))
            bgmSources.Add(source);
        source.volume = BgmVolume;
    }

    public void RegisterSe(AudioSource source)
    {
        if (!seSources.Contains(source))
            seSources.Add(source);
        source.volume = SeVolume;
    }

    public void UnregisterBgm(AudioSource source)
    {
        bgmSources.Remove(source);
    }

    public void UnregisterSe(AudioSource source)
    {
        seSources.Remove(source);
    }
}
