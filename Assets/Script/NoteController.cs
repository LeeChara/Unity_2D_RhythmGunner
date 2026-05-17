using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    RectTransform rectTransform;
    private float noteSpeed; // From GameManager
    private float destroyX;

    public float targetTick;
    public NoteType noteType;

    private bool isInitialized = false;
    void Update()
    {
        if (!isInitialized) return;
        rectTransform.anchoredPosition += Vector2.left * noteSpeed * Time.deltaTime;
        if (rectTransform.anchoredPosition.x < - destroyX)
        {
            GameManager.Instance.judgeSystem.OnMiss();
            Destroy(this.gameObject);
        }
    }
    public void Init(float targetTick, NoteType noteType, float noteSpeed, float destroyX)
    {
        this.targetTick = targetTick;
        this.noteType = noteType;
        this.noteSpeed = noteSpeed;
        this.destroyX = destroyX;
        rectTransform = this.GetComponent<RectTransform>();

        isInitialized = true;
    }
}
