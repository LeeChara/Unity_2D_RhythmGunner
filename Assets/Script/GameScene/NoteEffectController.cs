using UnityEngine;

public class NoteEffectController : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    private double startTick;
    private double duration;
    private RectTransform rectTransform;

    private bool isInitialized = false;
    void Update()
    {
        if (!isInitialized) return;
        float ratio = (float)((TickClock.Instance.Tick - startTick) / duration);
        rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, ratio);
    }

    public void Init(NoteEffectData noteEffectData)
    {
        float moveDistance = GameManager.Instance.laneController.moveDistance;
        float laneHeight = GameManager.Instance.laneController.hieght;

        Debug.Log($"[NoteEffectController] Init: moveDistance = {moveDistance}, laneHeight = {laneHeight}, startPosition = ({noteEffectData.startPosition.x}, {noteEffectData.startPosition.y}), endPosition = ({noteEffectData.endPosition.x}, {noteEffectData.endPosition.y})");

        this.startPosition.x = moveDistance * (noteEffectData.startPosition.x - 1);
        this.startPosition.y = laneHeight * (noteEffectData.startPosition.y - 0.5f);

        this.endPosition.x = moveDistance * (noteEffectData.endPosition.x - 1);
        this.endPosition.y = laneHeight * (noteEffectData.endPosition.y - 0.5f);

        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPosition;

        startTick = noteEffectData.tick;

        duration = noteEffectData.duration;

        Destroy(gameObject, noteEffectData.duration / (TickClock.Instance.Bpm / 60f * TickClock.Instance.Resolution)); // 오브젝트 제거

        isInitialized = true;
    }
}
