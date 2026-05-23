using UnityEngine;
using System.Collections.Generic;

public class NoteManager: MonoBehaviour
{
    // 노트 프리팹
    [SerializeField]
    private GameObject AttackNotePrefab;
    [SerializeField]
    private GameObject DefenseNotePrefab;
    [SerializeField]
    private GameObject CounterNotePrefab;
    [SerializeField]
    private GameObject ReloadNotePrefab;

    public RectTransform lane;

    private float arriveTick;
    private int resolution; // From GameManager
    private float destroyX; // 노트가 파괴되는 x 좌표
    private float moveDistance;

    public float noteSpeed { get; private set; }// From GameManager

    private List<NoteData> notes = new List<NoteData>();

    private bool isInitialized = false;
    private void Update()
    {
        if (!isInitialized) return; // 초기화 되기 전까지는 리턴

        // 현재 Tick에 스폰될 노트가 있으면 소환하고, 리스트에서 제거
        while (notes.Count > 0 && TickClock.Instance.Tick >= notes[0].tick)
        {
            SpawnNote(notes[0]);
            notes.RemoveAt(0);
            // Debug.Log($"[NoteSpawner] Spawned note at tick: {TickClock.Instance.Tick}, remaining notes: {notes.Count}");
        }
    }
    public void Init(int resolution, float noteSpeed, float arriveTick, float moveDistance)
    {
        this.resolution = resolution;
        this.noteSpeed = noteSpeed;
        this.arriveTick = arriveTick;
        this.moveDistance = moveDistance;
        this.destroyX = lane.rect.width;

        // Debug.Log($"[NoteSpawner] Initialized with resolution: {resolution}, noteSpeed: {noteSpeed}, arriveTick: {arriveTick}, destroyX: {destroyX}");
        isInitialized = true;
    }
    public void AddSchedule(NoteData noteData)
    {
        NoteData scheduledNoteData = new NoteData()
        {
            tick = noteData.tick - arriveTick, // 노트가 도착하는 tick보다 arriveTick만큼 먼저 스폰
            type = noteData.type,
            noteType = noteData.noteType,
            intensity = noteData.intensity
        };
        // Debug.Log($"[NoteSpawner] Scheduled Note - Original Tick: {noteData.tick}, Scheduled Tick: {scheduledNoteData.tick}, NoteType: {scheduledNoteData.noteType}, Intensity: {scheduledNoteData.intensity}");
        notes.Add(scheduledNoteData);
    }
    private void SpawnNote(NoteData noteData)
    {
        GameObject notePrefab = null;
        switch (noteData.noteType) // 노트 타입에 따른 프리팹 선택
        {
            case NoteType.Attack:
                notePrefab = AttackNotePrefab;
                break;
            case NoteType.Defense:
                notePrefab = DefenseNotePrefab;
                break;
            case NoteType.Counter:
                notePrefab = CounterNotePrefab;
                break;
            case NoteType.Reload:
                notePrefab = ReloadNotePrefab;
                break;
        }
        GameObject note = Instantiate(notePrefab);
        note.GetComponent<RectTransform>().SetParent(lane, false);
        note.GetComponent<NoteController>().Init(noteData.tick + arriveTick, noteData.noteType, moveDistance, destroyX);
    }

    // BPM이 변경될 때 노트 스피드와 arriveTick을 업데이트하는 메서드. BPMEventManager에서 호출됨
    public void ChangeBpm(float noteSpeed, float arriveTick)
    {
        this.noteSpeed = noteSpeed;
        this.arriveTick = arriveTick;
        Debug.Log($"[NoteSpawner] BPM Changed: noteSpeed = {noteSpeed}, arriveTick = {arriveTick}");
    }
}