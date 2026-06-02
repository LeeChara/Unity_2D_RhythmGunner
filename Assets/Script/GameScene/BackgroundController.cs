using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;
    private void Start()
    {
        startPos = transform.position.x;
    }
    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime * TickClock.Instance.Bpm * 0.03f, 0, 0);

        if(startPos - transform.position.x >= GetComponent<SpriteRenderer>().bounds.size.x)
        {
            
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
