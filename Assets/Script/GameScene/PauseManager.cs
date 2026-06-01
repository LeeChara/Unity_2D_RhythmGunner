using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        Instance = this;
        IsPaused = false;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!IsPaused) Pause();
            else Resume();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        TickClock.Instance.OnPause();
        GameManager.Instance.musicPlayer.Pause();
    }

    public void Resume()
    {
        IsPaused = false;
        TickClock.Instance.OnResume();
        GameManager.Instance.musicPlayer.Resume();
    }
}
