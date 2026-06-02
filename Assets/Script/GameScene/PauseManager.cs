using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public GameObject pauseWindow;
    public bool IsPaused { get; private set; }
    private bool isCountingDown = false;

    [SerializeField] private GameObject readyObject;
    [SerializeField] private GameObject setObject;
    [SerializeField] private GameObject goObject;

    private AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isCountingDown) return;
            if (!IsPaused) Pause();
            else Resume();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        pauseWindow.SetActive(true);
        TickClock.Instance.OnPause();
        GameManager.Instance.musicPlayer.Pause();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("StageSelection"); // °î ¼±ÅÃ ¾À ÀÌ¸§ ¸Â°Ô ¼öÁ¤
    }

    public void Resume()
    {
        pauseWindow.SetActive(false);
        StartCoroutine(CountdownResume());
    }

    private IEnumerator CountdownResume()
    {
        isCountingDown = true;

        audioSource.Play();
        readyObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        readyObject.SetActive(false);

        audioSource.Play();
        setObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        setObject.SetActive(false);

        audioSource.Play();
        goObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        goObject.SetActive(false);

        isCountingDown = false;
        IsPaused = false;
        TickClock.Instance.OnResume();
        GameManager.Instance.musicPlayer.Resume();
    }
}