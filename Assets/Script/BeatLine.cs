using UnityEngine;

/// <summary>
/// 레인에 생성되어, 박자를 가늠할 수 있게 하는 선입니다.
/// </summary>
public class BeatLine : MonoBehaviour
{
    RectTransform rt;
    public float noteSpeed = 1f;
    private float endX = -100f;
    void Update()
    {
        rt.anchoredPosition += (new Vector2(endX, 0) - rt.anchoredPosition).normalized * noteSpeed * Time.deltaTime;
        if (rt.anchoredPosition.x < endX)
        {
            Destroy(this.gameObject);
        }
    }

    public void Init(float startX, float endX)
    {
        this.endX = endX;
        rt = this.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(startX, 0);

        noteSpeed = Conductor.Instance.noteSpeed;
    }
}
