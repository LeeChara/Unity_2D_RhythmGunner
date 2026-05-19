using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
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
    private float noteSpeed; // From GameManager
    private float destroyX;
    private float moveDistance;

    private List<NoteData> notes = new List<NoteData>();

    private bool isInitialized = false;
    private void Update()
    {
        if (!isInitialized) return;

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
            tick = noteData.tick - arriveTick,
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
        switch (noteData.noteType)
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
        note.GetComponent<NoteController>().Init(noteData.tick + arriveTick, noteData.noteType, noteSpeed, moveDistance, destroyX);
    }
}