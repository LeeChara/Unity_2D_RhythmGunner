using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingWindow;
    
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OnEscape();
        }
    }
    public void OnSetting()
    {
        settingWindow.SetActive(true);
    }
    public void OnStart()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void OnExit()
    {
        Application.Quit();
    }

    private void OnEscape()
    {
        settingWindow.SetActive(false);
    }
}
