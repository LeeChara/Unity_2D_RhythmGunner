using UnityEngine;
using UnityEngine.UIElements;

public class RobotAlphaController : BossController
{
    public override void Execute(string bossAction)
    {
        switch (bossAction)
        {
            case "Appear":
                Appear();
                break;
            case "Disappear":
                Disappear();
                break;  
            case "SlashA":
                SlashA();
                break;
            default:
                Debug.LogWarning($"[BossController] Unknown boss action: {bossAction}");
                break;
        }
    }

    protected override void Appear()
    {
        Debug.Log($"[BossController] {bossType} appears!");
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.7f, 0.4f, 10.0f));
    }
    protected override void Disappear()
    {
        Debug.Log($"[BossController] {bossType} disappears!");
        Destroy(gameObject); // 보스 오브젝트 제거
    }
    private void SlashA()
    {
        Debug.Log($"[BossController] {bossType} uses SlashA!");
    }
}
