using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// 프로젝트 테스트용 클래스입니다. 실제 게임에서는 사용되지 않습니다.
/// </summary>
public class Tester : MonoBehaviour
{
    public ChartData chartData;
    void Start()
    {
        GameObject jsonReader = GameObject.Find("JSONReader");
        chartData = jsonReader.GetComponent<JSONReader>().Load("vs. DJ Subatomic Supernova (From No Straight Roads)");
        Debug.Log("Tester: " + chartData.metadata.title);
        Conductor.Instance.Init(chartData.metadata);


        GameObject lane = GameObject.Find("Lane");
    }

    void Update()
    {
        
    }
}
