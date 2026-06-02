using UnityEngine;

public class BeatLineController : MonoBehaviour
{
    private RectTransform rectTransform;
    private float destroyX;
    private float moveDistance;
    public float targetTick;
    private NoteManager noteManager;
    private bool isInitialized = false;

    void Update()
    {
        if (!isInitialized) return;

        float x = (float)((targetTick - TickClock.Instance.Tick) * noteManager.noteSpeed
            / ((TickClock.Instance.Bpm / 60f) * TickClock.Instance.Resolution)) - moveDistance;
        rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);

        if (rectTransform.anchoredPosition.x < -destroyX)
        {
            Destroy(gameObject);
        }
    }

    public void Init(float targetTick, float moveDistance, float destroyX)
    {
        this.targetTick = targetTick;
        this.moveDistance = moveDistance;
        this.destroyX = destroyX;
        rectTransform = GetComponent<RectTransform>();
        noteManager = GameManager.Instance.noteManager;
        isInitialized = true;
    }
}