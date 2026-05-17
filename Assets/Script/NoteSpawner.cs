using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject notePrefab;

    public RectTransform lane;

    private float spawnTick;
    private float arriveTick;
    private int resolution; // From GameManager
    private float noteSpeed; // From GameManager
    private float destroyX;

    private bool isInitialized = false;
    private void Update()
    {
        if (!isInitialized) return;

        while (TickClock.Instance.Tick >= spawnTick)
        {
            SpawnNote(NoteType.Reload);
            spawnTick += resolution;
        }
    }

    private void SpawnNote(NoteType noteType)
    {
        GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);
        note.GetComponent<RectTransform>().SetParent(lane, false);
        note.GetComponent<NoteController>().Init(TickClock.Instance.Tick + arriveTick, noteType, noteSpeed, destroyX); // Todo : NoteType should be determined by the note data
    }

    public void Init(int resolution, float noteSpeed, float arriveTick)
    {
        this.resolution = resolution;
        this.noteSpeed = noteSpeed;
        this.arriveTick = arriveTick;
        spawnTick = - arriveTick;
        this.destroyX = lane.rect.width;

        isInitialized = true;
    }
}