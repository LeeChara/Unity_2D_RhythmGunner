using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoModeButton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private void Start()
    {
        UpdateVisual();
    }

    public void OnClick()
    {
        SettingManager.Instance.SetIsAuto(!SettingManager.Instance.IsAuto);
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        bool isAuto = SettingManager.Instance.IsAuto;
        buttonImage.color = isAuto ? activeColor : inactiveColor;
        buttonText.color = isAuto ? activeColor : inactiveColor;
    }
}