using UnityEngine;
public class JudgeSystem : MonoBehaviour
{
    private float perfectTick; // perfect 판정 tick 범위
    private float goodTick; // good 판정 tick 범위
    private float missTick; // miss 판정 tick 범위
    private Transform lane;

    public Transform judgementLine;
    public GameObject judgePerfectPrefab;
    public GameObject judgeGoodPrefab;
    public GameObject judgeMissPrefab;
    public float judgeEffectDistance;
    public void Init(float perfectTick, float goodTick, float missTick, Transform lane)
    {
        this.perfectTick = perfectTick;
        this.goodTick = goodTick;
        this.missTick = missTick;
        this.lane = lane;

        Debug.Log($"[JudgeSystem] Initialized with perfectTick: {perfectTick}, goodTick: {goodTick}, badTick: {missTick}");
    }

    // noteType과 키 입력 상태를 받아 판정을 수행하는 메서드. noteType은 판정하려는 노트의 유형, isKeyDown은 키가 눌렸는지 여부를 나타냄.
    public bool Judge(NoteType noteType, bool isKeyDown = false)
    {
        float currentTick = (float) TickClock.Instance.Tick; // 입력 시점의 tick

        // 소환되어있는 노트들 중 currentTick과 가장 가까운 노트를 반환
        // noteType이 일치하거나, noteType이 Counter가 아니면서 소환된 노트의 noteType이 Reload인 경우만 고려 (즉, Counter 입력으로는 Reload 노트를 판정할 수 없음)
        NoteController closestNote = null;
        float minTickDiff = float.MaxValue;
        foreach (Transform note in lane)
        {
            NoteController nc = note.GetComponent<NoteController>();
            if (nc == null) continue;
            float tickDiff = Mathf.Abs(nc.targetTick - currentTick);
            if (tickDiff < minTickDiff && (nc.noteType == noteType || (nc.noteType == NoteType.Reload && noteType != NoteType.Counter)))
            {
                closestNote = nc;
                minTickDiff = tickDiff;
            }
        }

        if (closestNote == null) return false; // 판정 가능한 노트가 없는 경우 리턴
        if (closestNote.noteType == NoteType.Reload && minTickDiff > perfectTick && isKeyDown) return false; // Reload 노트는 키다운인 경우 perfect 판정만 가능
        if (minTickDiff > missTick) return false; // miss 판정 범위를 넘어선 경우 리턴

        if (minTickDiff <= perfectTick) // perfect 판정 범위 내에 있는 경우
        {
            OnPerfect(closestNote.targetTick);
        }
        else if (minTickDiff <= goodTick) // good 판정 범위 내에 있는 경우
        {
            OnGood(closestNote.targetTick);
        }
        else if (minTickDiff <= missTick) // miss 판정 범위 내에 있는 경우
        {
            OnMiss();
        }
        Destroy(closestNote.gameObject); // 판정된 노트 제거
        return true;
    }

    private void OnPerfect(float tick)
    {
        Debug.Log("[JudgeSystem] Perfect!");
        GameManager.Instance.resultManager.OnPerfect();
        GameManager.Instance.enemyManager.DestroyEnemy(tick); // 노트와 동일한 tick에 소환된 적 제거
        Vector3 position = GameManager.Instance.laneController.getJudgementLinePosition();
        position.y += judgeEffectDistance;
        Debug.Log(position);
        Debug.Log(judgementLine.GetComponent<RectTransform>().anchoredPosition);
        GameObject judgeEffect = Instantiate(judgePerfectPrefab, position, Quaternion.identity, judgementLine);
        judgeEffect.GetComponent<JudgeEffectController>().Init(position); // 0, 50, 0 전달해야함 (아직 구현x)
    }

    private void OnGood(float tick)
    {
        Debug.Log("[JudgeSystem] Good!");
        GameManager.Instance.resultManager.OnGood();
        GameManager.Instance.enemyManager.DestroyEnemy(tick); // 노트와 동일한 tick에 소환된 적 제거
        Vector3 position = GameManager.Instance.laneController.getJudgementLinePosition();
        position.y += judgeEffectDistance;
        GameObject judgeEffect = Instantiate(judgeGoodPrefab, position, Quaternion.identity, judgementLine);
        judgeEffect.GetComponent<JudgeEffectController>().Init(position);
    }

    public void OnMiss()
    {
        Debug.Log("[JudgeSystem] Miss!");
        GameManager.Instance.resultManager.OnMiss();
        Vector3 position = GameManager.Instance.laneController.getJudgementLinePosition();
        position.y += judgeEffectDistance;
        GameObject judgeEffect = Instantiate(judgeMissPrefab, position, Quaternion.identity, judgementLine);
        judgeEffect.GetComponent<JudgeEffectController>().Init(position);
    }
}
