using UnityEngine;

public class BossController : MonoBehaviour
{
    public string bossType;
    public virtual void Execute(string bossAction) { }
    protected virtual void Appear() { }
    protected virtual void Disappear() { }
}
