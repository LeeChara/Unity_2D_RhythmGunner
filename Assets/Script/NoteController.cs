using UnityEngine;

public class NoteController : MonoBehaviour
{
    RectTransform rectTransform;
    private float noteSpeed; // From GameManager
    private float destroyX;

    private bool isInitialized = false;
    void Update()
    {
        if (!isInitialized) return;
        rectTransform.anchoredPosition += Vector2.left * noteSpeed * Time.deltaTime;
        if (rectTransform.anchoredPosition.x < - destroyX)
        {
            Destroy(this.gameObject);
        }
    }
    public void Init(float noteSpeed, float destroyX)
    {
        this.noteSpeed = noteSpeed;
        this.destroyX = destroyX;
        rectTransform = this.GetComponent<RectTransform>();

        isInitialized = true;
    }
}
