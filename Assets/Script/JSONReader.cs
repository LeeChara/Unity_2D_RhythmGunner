using UnityEngine;

/// <summary>
/// Chart data가 저장된 JSON 파일을 읽고, ChartData 객체로 변환하는 클래스입니다.
/// </summary>
public class JSONReader : MonoBehaviour
{
    void Start()
    {
        // 테스트용 코드
        Load("vs. DJ Subatomic Supernova (From No Straight Roads)");
    }
    public ChartData Load(string filename)
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Chart/{filename}");
        if (textAsset == null)
        {
            Debug.LogError($"Failed to load chart: {filename}");
            return null;
        }
        ChartData chartData = JsonUtility.FromJson<ChartData>(textAsset.text);
        Debug.Log("JSONReader: " + chartData.metadata.title);
        return chartData;
    }
}
