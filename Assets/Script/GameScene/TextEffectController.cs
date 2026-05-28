using UnityEngine;
using TMPro;

public class TextEffectController : MonoBehaviour
{
    private RectTransform canvas;
    private TextMeshProUGUI textMeshPro;
    public void Init(TextEffectData textEffectData, RectTransform canvas)
    {
        this.canvas = canvas;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(
            textEffectData.position.x * canvas.sizeDelta.x - canvas.sizeDelta.x / 2,
            textEffectData.position.y * canvas.sizeDelta.y - canvas.sizeDelta.y / 2
        );
        string text = textEffectData.text;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = text;
        textMeshPro.fontSize = textEffectData.size;
        Color color = ColorUtility.TryParseHtmlString(textEffectData.color, out color) ? color : Color.white;
        textMeshPro.color = color;
        textMeshPro.alignment = textEffectData.align switch
        {
            "Left" => TextAlignmentOptions.Left,
            "Center" => TextAlignmentOptions.Center,
            "Right" => TextAlignmentOptions.Right,
            _ => TextAlignmentOptions.Center
        };

        Destroy(this.gameObject, textEffectData.duration); // duration이 지난 후 텍스트 효과 오브젝트 파괴
    }
}
