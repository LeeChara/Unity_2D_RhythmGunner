using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }
    public float JudgementOffset; // tick 단위, 판정 타이밍 조절, 양수: 일찍 눌러야함, 음수: 늦게 눌러야함
    public float NoteSpawnOffset; // ms 단위, 노트 소환 타이밍 조절, 양수: 노트가 빨리 소환, 음수: 노트가 늦게 소환 
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetJudgementOffset(float offset)
    {
        JudgementOffset = offset;
    }

    public void SetNoteSpawnOffset(float offset)
    {
        NoteSpawnOffset = offset;
    }
}
