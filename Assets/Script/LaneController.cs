using UnityEngine;

public class LaneController : MonoBehaviour
{
    private float moveDistance;
    private float noteSpeed; // From GameManager

    public GameObject judgementLine;

    public void Init(float noteSpeed)
    {
        this.noteSpeed = noteSpeed;
        moveDistance = this.GetComponent<RectTransform>().rect.width - judgementLine.GetComponent<RectTransform>().anchoredPosition.x;  
    }
    public float getArriveTime()
    {
        float arriveTime = moveDistance / noteSpeed;
        Debug.Log("LaneController getArriveTime: moveDistance = " + moveDistance + ", noteSpeed = " + noteSpeed + ", arriveTime = " + arriveTime);
        return arriveTime;
    }
}
