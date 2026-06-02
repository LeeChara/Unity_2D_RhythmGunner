using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AlertEffectController : MonoBehaviour
{
    [SerializeField] private Light2D alert;

    public void Init()
    {
        alert.enabled = false;
    }

    public void Alert(float duration)
    {
        alert.enabled = true;
        Invoke("TurnOff", duration);
    }

    private void TurnOff()
    {
        alert.enabled = false;
    }
}
