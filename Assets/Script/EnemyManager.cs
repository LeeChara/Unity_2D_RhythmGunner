using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    // 적 프리팹
    [SerializeField]
    private GameObject RobotAPrefab;

    private List<EnemyData> enemies = new List<EnemyData>(); // ChartScheduler로부터 받은 스케줄링된 적 데이터 리스트

    [SerializeField]
    private List<EnemyController> spawnedEnemies = new List<EnemyController>();
    private Vector2 spawnPosition;
    private float prepareTick; // Tick 단위, 시작하기 전 준비 시간

    public void Init()
    {
        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(1.5f, 1.5f, 0f)); // 화면 밖에서 스폰, 정해진 타이밍에 화면 안으로 이동
        prepareTick = 8 * TickClock.Instance.Resolution; // 8박자
    }
    private void Update()
    {
        // 현재 Tick에 스폰될 적이 있으면 소환하고, 리스트에서 제거
        while (enemies.Count > 0 && TickClock.Instance.Tick >= enemies[0].tick)
        {
            SpawnEnemy(enemies[0]);
            enemies.RemoveAt(0);
        }
    }
    
    // 스케줄 추가 메서드. ChartScheduler에서 호출됨
    public void AddSchedule(EnemyData enemyData)
    {
        EnemyData scheduledEnemyData = new EnemyData()
        {
            tick = enemyData.tick - prepareTick, // 준비 시간만큼 먼저 스폰
            type = enemyData.type,
            enemyType = enemyData.enemyType,
            position = enemyData.position
        };
        // Debug.Log($"[EnemySpawner] Scheduled Enemy - Tick: {scheduledEnemyData.tick}, EnemyType: {scheduledEnemyData.enemyType}");
        enemies.Add(scheduledEnemyData);
    }


    // 적 소환
    private void SpawnEnemy(EnemyData enemyData) 
    {
        GameObject enemyPrefab = null;
        switch (enemyData.enemyType) // 적 타입에 따른 프리팹 선택
        {
            case "RobotA":
                enemyPrefab = RobotAPrefab;
                break;
            default:
                //Debug.LogWarning($"[EnemySpawner] Unknown enemy type: {enemyData.enemyType}");
                return;
                // Add more enemy types here
        }
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Init(enemyData.tick + prepareTick, enemyData.position);
        spawnedEnemies.Add(enemyController);
        //Debug.Log($"[EnemySpawner] Spawned enemy of type {enemyData.enemyType} at tick: {enemyData.tick}, scheduled to appear at tick: {enemyData.tick + prepareTick}");
    }

    // 적 제거 메서드, Judge에서 호출됨. 판정된 tick과 일치하는 적을 제거
    public void DestroyEnemy(float tick)
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            //Debug.Log($"[EnemySpawner] Enemy targetTick: {spawnedEnemies[i].targetTick}, tick: {tick}");
            if (spawnedEnemies[i].targetTick == tick)
            {
                spawnedEnemies[i].Die();
                spawnedEnemies.RemoveAt(i);
            }
        }
    }
}
