using UnityEngine;

/// <summary>
/// Notes를 RuntimeNote로 변환하는 클래스입니다.
/// </summary>
public class NoteTranslator : MonoBehaviour
{
    public RectTransform judgeLine;
    public void Translate(NoteData[] notes)
    {
        foreach (NoteData note in notes)
        {
            // 아직 미구현 상태
        }
    }

    private float CalculateDistance()
    {
        float distance = this.GetComponent<RectTransform>().rect.width - judgeLine.anchoredPosition.x;
        return distance;
    }
}
