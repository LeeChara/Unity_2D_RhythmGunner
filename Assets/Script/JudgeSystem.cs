using UnityEngine;

public class JudgeSystem : MonoBehaviour
{
    private float perfectTick, goodTick;
    public void Init(float perfectTick, float goodTick) //perfectTick, goodTick 매개변수는 GameManager로부터 전달받음
    {
        this.perfectTick = perfectTick;
        this.goodTick = goodTick;
    } //전달받은 변수를 JudgeSystem의 맴버변수에 저장

    public Transform GetClosestNote(Transform lane)
    {
        Transform closestNote = null;
        foreach (Transform note in lane) //레인 안의 모든 노트들을 순회
        {

        }
        return closestNote;
    } //어떤 기능을 하는 함수인지 모르겠습니다.
}
