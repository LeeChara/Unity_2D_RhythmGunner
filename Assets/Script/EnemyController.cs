using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float appearBeat; // Beat
    [SerializeField] private float prepareBeat; // Beat
    [SerializeField] private float attackBeat; // Beat
    [SerializeField] private float disappearBeat; // Beat

    private float appearTick;
    private float prepareTick;
    private float attackTick;
    private float disappearTick;

    private bool isAppeared = false;
    private bool isPrepared = false;
    private bool isAttacked = false;
    private bool isDisappeared = false;

    public float targetTick { get; private set; }
    private Position position;
    public void Init(float targetTick, Position position)
    {
        appearTick = targetTick - appearBeat * TickClock.Instance.Resolution;
        prepareTick = targetTick - prepareBeat * TickClock.Instance.Resolution;
        attackTick = targetTick - attackBeat * TickClock.Instance.Resolution;
        disappearTick = targetTick - disappearBeat * TickClock.Instance.Resolution;

        this.position = position;
    }
    private void Update()
    {
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
        Debug.Log("[EnemyController] Enemy Appeared at Tick: " + TickClock.Instance.Tick);
    }
    private void Prepare()
    {
        Debug.Log("[EnemyController] Enemy Prepared at Tick: " + TickClock.Instance.Tick);
    }
    private void Attack()
    {
        Debug.Log("[EnemyController] Enemy Attacked at Tick: " + TickClock.Instance.Tick);
    }
    private void Disappear()
    {
        Debug.Log("[EnemyController] Enemy Disappeared at Tick: " + TickClock.Instance.Tick);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
