using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    RectTransform rectTransform;
    private float noteSpeed; // From GameManager, pixels per second
    private float destroyX;
    private float moveDistance; // From LaneController, pixels

    public float targetTick;
    public NoteType noteType;

    private bool isInitialized = false;
    void Update()
    {
        if (!isInitialized) return;

        float x = (float) ((targetTick - TickClock.Instance.Tick) * noteSpeed / ((TickClock.Instance.Bpm / 60f) * TickClock.Instance.Resolution)) - moveDistance;
        rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);

        if (rectTransform.anchoredPosition.x < - destroyX)
        {
            GameManager.Instance.judgeSystem.OnMiss();
            Destroy(this.gameObject);
        }
    }
    public void Init(float targetTick, NoteType noteType, float noteSpeed, float moveDistance, float destroyX)
    {
        this.targetTick = targetTick;
        this.noteType = noteType;
        this.noteSpeed = noteSpeed;
        this.moveDistance = moveDistance;
        this.destroyX = destroyX;
        rectTransform = this.GetComponent<RectTransform>();

        isInitialized = true;
    }
}
