using UnityEngine;

public class LaneController : MonoBehaviour
{
    private float moveDistance;
    private float noteSpeed; // From GameManager

    public GameObject judgementLine;

    public void Init(float noteSpeed) //noteSpeed 매개변수는 GameManager로부터 전달받음
    {
        this.noteSpeed = noteSpeed;
        moveDistance = this.GetComponent<RectTransform>().rect.width - judgementLine.GetComponent<RectTransform>().anchoredPosition.x;
    } //노트 속도 및 노트 이동 거리 초기화
    public float getArriveTime()
    {
        return moveDistance / noteSpeed;
    } //노트가 판정선에 도달하는 시간 계산
}
