using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private string bossType;
    public string BossType { get { return bossType; } } // 읽기 전용, Inspector에서 설정 가능
    public virtual void Execute(string bossAction) { }
    protected virtual void Appear() { }
    protected virtual void Disappear() { }
}
