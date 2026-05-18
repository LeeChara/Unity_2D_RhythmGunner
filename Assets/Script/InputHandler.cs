using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Key[] attackKeys = { Key.A, Key.S, Key.D, Key.F, Key.U, Key.I };
    private Key[] defenseKeys = { Key.J, Key.K, Key.L, Key.Semicolon, Key.E, Key.R};
    private Key[] counterKeys = { Key.Space };
    void Update()
    {
        foreach (Key key in counterKeys)
        {
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                GameManager.Instance.judgeSystem.Judge(NoteType.Counter);
            }
        }

        foreach (Key key in attackKeys)
        {
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                if (!GameManager.Instance.judgeSystem.Judge(NoteType.Attack))
                {
                    GameManager.Instance.judgeSystem.Judge(NoteType.Reload);
                }
                return;
            }
        }

        foreach (Key key in defenseKeys)
        {
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                if (!GameManager.Instance.judgeSystem.Judge(NoteType.Defense))
                {
                    GameManager.Instance.judgeSystem.Judge(NoteType.Reload);
                }
                return;
            }
        }

        foreach (Key key in attackKeys)
        {
            if (Keyboard.current[key].isPressed)
            {
                GameManager.Instance.judgeSystem.Judge(NoteType.Reload, true);
                return;
            }
        }

        foreach (Key key in defenseKeys)
        {
            if (Keyboard.current[key].isPressed)
            {
                GameManager.Instance.judgeSystem.Judge(NoteType.Reload, true);
                return;
            }
        }
    }
}
