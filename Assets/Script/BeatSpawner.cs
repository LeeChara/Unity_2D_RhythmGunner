using UnityEngine;

/// <summary>
/// BeatLine을 일정 간격으로 생성하는 클래스입니다.
/// </summary>
public class BeatSpawner : MonoBehaviour
{
    public GameObject beatPrefab;
    float spawnTick;
    float spawnInterval;

    bool isInitialized = false;
    void Update()
    {
        if (!isInitialized) return;

        while (Conductor.Instance.currentTick >= spawnTick)
        {
            SpawnBeat();
            spawnTick += spawnInterval;
        }
    }

    public void Init()
    {
        spawnInterval = Conductor.Instance.resolution;
        spawnTick = - 10 * spawnInterval;
        Debug.Log("BeatSpawner: Spawn interval set to " + spawnInterval);

        isInitialized = true;
    }

    void SpawnBeat()
    {
        float xMax = GetComponent<RectTransform>().rect.xMax;
        float xMin = GetComponent<RectTransform>().rect.xMin;

        GameObject beatLine = Instantiate(beatPrefab, transform);

        beatLine.GetComponent<BeatLine>().Init(xMax, xMin);
    }
}
