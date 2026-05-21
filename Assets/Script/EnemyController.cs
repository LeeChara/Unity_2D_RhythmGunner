using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float appearBeat; // 박자 단위, 등장 타이밍
    [SerializeField] private float prepareBeat; // 박자 단위, 공격 준비 타이밍
    [SerializeField] private float attackBeat; // 박자 단위, 공격 타이밍
    [SerializeField] private float disappearBeat; // 박자 단위, 퇴장 타이밍


    private float appearTick;
    private float prepareTick;
    private float attackTick;
    private float disappearTick;

    // 상태 추적용 변수
    private bool isAppeared = false;
    private bool isPrepared = false;
    private bool isAttacked = false;
    private bool isDisappeared = false;

    public float targetTick { get; private set; } // 판정 타이밍 Tick
    private Position position;
    public void Init(float targetTick, Position position)
    {
        // 박자를 Tick으로 변환
        appearTick = targetTick - appearBeat * TickClock.Instance.Resolution;
        prepareTick = targetTick - prepareBeat * TickClock.Instance.Resolution;
        attackTick = targetTick - attackBeat * TickClock.Instance.Resolution;
        disappearTick = targetTick - disappearBeat * TickClock.Instance.Resolution;

        this.position = position;
    }
    private void Update()
    {
        // 각 타이밍에 맞춰 행동 실행
        if (!isAppeared && TickClock.Instance.Tick >= appearTick)
        {
            Appear();
            isAppeared = true;
        }

        if (!isPrepared && TickClock.Instance.Tick >= prepareTick)
        {
            Prepare();
            isPrepared = true;
        }

        if (!isAttacked && TickClock.Instance.Tick >= attackTick)
        {
            Attack();
            isAttacked = true;
        }

        if (!isDisappeared && TickClock.Instance.Tick >= disappearTick)
        {
            Disappear();
            isDisappeared = true;
        }
    }

    private void Appear() // 등장
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(position.x, position.y, 10));
        Debug.Log("[EnemyController] Enemy Appeared at Tick: " + TickClock.Instance.Tick);
    }
    private void Prepare() // 공격 준비
    {
        Debug.Log("[EnemyController] Enemy Prepared at Tick: " + TickClock.Instance.Tick);
    }
    private void Attack() // 공격
    {
        Debug.Log("[EnemyController] Enemy Attacked at Tick: " + TickClock.Instance.Tick);
    }
    private void Disappear() // 퇴장
    {
        Debug.Log("[EnemyController] Enemy Disappeared at Tick: " + TickClock.Instance.Tick);
    }

    public void Die() // 사망
    {
        Destroy(this.gameObject);
    }
}
