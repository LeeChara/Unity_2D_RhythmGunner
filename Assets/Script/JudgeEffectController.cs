using UnityEngine;

public class JudgeEffectController : MonoBehaviour
{
    public float moveDistance;
    public float duration;

    private Vector3 endPos;
    private RectTransform rectTransform;
    private void Update()
    {
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, endPos, moveDistance / duration * Time.deltaTime);
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        endPos = rectTransform.anchoredPosition;
        endPos.y -= moveDistance;
        Destroy(gameObject, duration);
    }
}
