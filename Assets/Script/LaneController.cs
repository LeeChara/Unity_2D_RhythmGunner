using UnityEngine;

public class LaneController : MonoBehaviour
{
    public float moveDistance;
    private float noteSpeed; // From GameManager

    public GameObject judgementLine;

    public void Init(float noteSpeed) //noteSpeed 매개변수는 GameManager로부터 전달받음
    {
        this.noteSpeed = noteSpeed;
        // 노트의 이동 거리 계산: Lane의 너비와 판정선의 x 좌표의 차
        moveDistance = this.GetComponent<RectTransform>().rect.width - judgementLine.GetComponent<RectTransform>().anchoredPosition.x;  
    }

    public float getArriveTime() // 노트가 판정선에 도달하는 시간 계산
    {
        float arriveTime = moveDistance / noteSpeed;
        Debug.Log("[LaneController] Initialized with getArriveTime: moveDistance = " + moveDistance + ", noteSpeed = " + noteSpeed + ", arriveTime = " + arriveTime);
        return arriveTime;
    }
}
