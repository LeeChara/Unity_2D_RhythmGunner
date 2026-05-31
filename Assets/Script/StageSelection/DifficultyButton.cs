using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [Header("Difficulty")]
    public Difficulty difficulty;

    [Header("Image")]
    public Image buttonImage;

    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite selectedSprite;

    private SongPreview preview;

    private void Start()
    {
        preview = GetComponentInParent<SongPreview>();

        UpdateVisual();
    }

    public void OnClick()
    {
        if (preview != null)
        {
            preview.SelectDifficulty(difficulty);
        }
    }

    public void UpdateVisual()
    {
        if (preview == null)
            return;

        bool isSelected =
            preview.CurrentDifficulty == difficulty;

        if (isSelected)
        {
            buttonImage.sprite = selectedSprite;

            transform.localScale =
                Vector3.one * 1.15f;
        }
        else
        {
            buttonImage.sprite = normalSprite;

            transform.localScale =
                Vector3.one;
        }
    }
}