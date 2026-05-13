using UnityEngine;

public class JudgeSystem : MonoBehaviour
{
    private float perfectTick, goodTick;
    public void Init(float perfectTick, float goodTick)
    {
        this.perfectTick = perfectTick;
        this.goodTick = goodTick;
    }

    public Transform GetClosestNote(Transform lane)
    {
        Transform closestNote = null;
        foreach (Transform note in lane)
        {

        }
        return closestNote;
    }
}
