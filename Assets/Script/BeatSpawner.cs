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
        spawnTick = spawnInterval;
        Debug.Log("BeatSpawner: Spawn interval set to " + spawnInterval);

        isInitialized = true;
    }

    void SpawnBeat()
    {
        float halfWidth = GetComponent<RectTransform>().rect.width / 2f;

        GameObject left = Instantiate(beatPrefab, transform);
        GameObject right = Instantiate(beatPrefab, transform);

        left.GetComponent<BeatLine>().Init(-halfWidth);
        right.GetComponent<BeatLine>().Init(halfWidth);
    }
}
