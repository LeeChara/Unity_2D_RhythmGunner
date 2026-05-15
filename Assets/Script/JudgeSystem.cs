using UnityEngine;
public class JudgeSystem : MonoBehaviour
{
    private float perfectTick;
    private float goodTick;
    private float missTick;
    private Transform lane;
    public void Init(float perfectTick, float goodTick, float missTick, Transform lane)
    {
        this.perfectTick = perfectTick;
        this.goodTick = goodTick;
        this.missTick = missTick;
        this.lane = lane;

        Debug.Log($"JudgeSystem initialized with perfectTick: {perfectTick}, goodTick: {goodTick}, badTick: {missTick}");
    }

    public bool Judge(NoteType noteType)
    {
        Debug.Log($"Judging note of type {noteType} at tick {TickClock.Instance.Tick}");

        NoteController closestNote = null;
        float minTickDiff = float.MaxValue;
        foreach (Transform note in lane)
        {
            NoteController nc = note.GetComponent<NoteController>();
            if (nc == null) continue;
            float tickDiff = Mathf.Abs(nc.targetTick - TickClock.Instance.Tick);
            if (tickDiff < minTickDiff && (nc.noteType == noteType || (nc.noteType == NoteType.Reload && noteType != NoteType.Counter)))
            {
                closestNote = nc;
                minTickDiff = tickDiff;
            }
        }

        if (closestNote == null) return false;
        if (minTickDiff > missTick) return false;

        if (minTickDiff <= perfectTick)
        {
            OnPerfect();
        }
        else if (minTickDiff <= goodTick)
        {
            OnGood();
        }
        else if (minTickDiff <= missTick)
        {
            OnMiss();
        }
        Destroy(closestNote.gameObject);
        return true;
    }

    private void OnPerfect()
    {
        Debug.Log("Perfect!");
    }

    private void OnGood()
    {
        Debug.Log("Good!");
    }

    public void OnMiss()
    {
        Debug.Log("Miss!");
    }
}
