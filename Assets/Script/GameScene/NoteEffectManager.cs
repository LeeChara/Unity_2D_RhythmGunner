using UnityEngine;
using System.Collections.Generic;

public class NoteEffectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackNoteEffectPrefab;
    [SerializeField]
    private GameObject DefenseNoteEffectPrefab;
    [SerializeField]
    private GameObject CounterNoteEffectPrefab;
    [SerializeField]
    private GameObject ReloadNoteEffectPrefab;

    private List<NoteEffectData> noteEffects = new List<NoteEffectData>();

    public RectTransform lane;
     private void Update()
    {
        // 현재 Tick에 스폰될 노트 효과가 있으면 소환하고, 리스트에서 제거
        while (noteEffects.Count > 0 && TickClock.Instance.Tick >= noteEffects[0].tick)
        {
            SpawnNoteEffect(noteEffects[0]);
            noteEffects.RemoveAt(0);
        }
    }
    public void AddSchedule(NoteEffectData noteEffectData)
    {
        noteEffects.Add(noteEffectData);
    }

    private void SpawnNoteEffect(NoteEffectData noteEffectData)
    {
        GameObject noteEffectPrefab = null;
        switch (noteEffectData.noteType) // 노트 타입에 따른 프리팹 선택
        {
            case "Attack":
                noteEffectPrefab = AttackNoteEffectPrefab;
                break;
            case "Defense":
                noteEffectPrefab = DefenseNoteEffectPrefab;
                break;
            case "Counter":
                noteEffectPrefab = CounterNoteEffectPrefab;
                break;
            case "Reload":
                noteEffectPrefab = ReloadNoteEffectPrefab;
                break;
        }

        GameObject noteEffect = Instantiate(noteEffectPrefab, Vector3.zero, Quaternion.identity, lane);
        noteEffect.GetComponent<NoteEffectController>().Init(noteEffectData);
    }

    public void SkipEvent(float jumpTick)
    {
        for (int i = noteEffects.Count - 1; i >= 0; i--)
        {
            if (noteEffects[i].tick <= jumpTick)
            {
                noteEffects.RemoveAt(i);
            }
        }
    }
}
