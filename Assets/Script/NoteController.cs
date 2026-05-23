using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private NoteManager noteManager;
    private RectTransform rectTransform;
    private float destroyX;
    private float moveDistance; // From LaneController, pixels

    public float targetTick;
    public NoteType noteType;

    private bool isInitialized = false;
    void Update()
    {
        if (!isInitialized) return;

        // 노트 위치를 현재 Tick에 따라 업데이트. Tick이 음악에 동기화되어 있기 때문에 노트도 항상 음악과 동기화됨
        float x = (float) ((targetTick - TickClock.Instance.Tick) * noteManager.noteSpeed / ((TickClock.Instance.Bpm / 60f) * TickClock.Instance.Resolution)) - moveDistance;
        rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);

        if (rectTransform.anchoredPosition.x < - destroyX)
        {
            // 노트가 판정 라인을 지나쳐서 사라지는 경우, Miss 판정
            GameManager.Instance.judgeSystem.OnMiss();
            Destroy(this.gameObject);
        }
    }
    public void Init(float targetTick, NoteType noteType, float moveDistance, float destroyX)
    {
        this.targetTick = targetTick;
        this.noteType = noteType;
        this.moveDistance = moveDistance;
        this.destroyX = destroyX;
        rectTransform = this.GetComponent<RectTransform>();
        this.noteManager = GameManager.Instance.noteManager;

        isInitialized = true;
    }
}
