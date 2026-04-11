using UnityEngine;

/// <summary>
/// 레인에 생성되어, 박자를 가늠할 수 있게 하는 선입니다.
/// </summary>
public class BeatLine : MonoBehaviour
{
    RectTransform rt;
    public float speed = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rt.anchoredPosition += (Vector2.zero - rt.anchoredPosition).normalized * speed * Time.deltaTime;
        if ((rt.anchoredPosition - Vector2.zero).magnitude < 0.5f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Init(float startX)
    {
        rt = this.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(startX, 0);
    }
}
