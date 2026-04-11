using UnityEngine;

/// <summary>
/// 프로젝트 테스트용 클래스입니다. 실제 게임에서는 사용되지 않습니다.
/// </summary>
public class Tester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject jsonReader = GameObject.Find("JSONReader");
        ChartData chartData = jsonReader.GetComponent<JSONReader>().Load("vs. DJ Subatomic Supernova (From No Straight Roads)");
        Debug.Log("Tester: " + chartData.metadata.title);
        Conductor.Instance.Init(chartData.metadata);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
