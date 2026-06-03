using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    private Key[] trigger = { Key.Space, Key.Enter, Key.Escape };
    private void Update()
    {
        foreach (Key key in trigger)
        {
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                OnBackButton();
            }
        }
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("StageSelection");
    }
}
