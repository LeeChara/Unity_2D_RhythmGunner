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
    private string enemyType;
    private Animator animator;
    public void Init(string enemyType ,float targetTick, Position position)
    {
        this.targetTick = targetTick;
        this.enemyType = enemyType;
        // 박자를 Tick으로 변환
        appearTick = targetTick - appearBeat * TickClock.Instance.Resolution;
        prepareTick = targetTick - prepareBeat * TickClock.Instance.Resolution;
        attackTick = targetTick - attackBeat * TickClock.Instance.Resolution;
        disappearTick = targetTick - disappearBeat * TickClock.Instance.Resolution;

        this.position = position;
        animator = GetComponent<Animator>();
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

    private void Appear()
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(position.x, position.y, 10));
        animator.SetTrigger("Appear");
    }
    private void Prepare()
    {
        animator.SetTrigger("Prepare");
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void Disappear()
    {
        animator.SetTrigger("Disappear");
    }
    public void Die()
    {
        animator.SetTrigger("Disappear");
        Destroy(gameObject);
    }
}
