using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject RobotAPrefab;

    private List<EnemyData> enemies = new List<EnemyData>();
    private List<EnemyController> spawnedEnemies = new List<EnemyController>();
    private Vector2 spawnPosition;
    private float prepareTick;

    public void Init()
    {
        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(1.5f, 1.5f, 0f));
        prepareTick = 8 * TickClock.Instance.Resolution;
    }
    private void Update()
    {
        while (enemies.Count > 0 && TickClock.Instance.Tick >= enemies[0].tick)
        {
            SpawnEnemy(enemies[0]);
            enemies.RemoveAt(0);
        }
    }

    public void AddSchedule(EnemyData enemyData)
    {
        EnemyData scheduledEnemyData = new EnemyData()
        {
            tick = enemyData.tick - prepareTick,
            type = enemyData.type,
            enemyType = enemyData.enemyType,
            position = enemyData.position
        };
        Debug.Log($"[EnemySpawner] Scheduled Enemy - Tick: {scheduledEnemyData.tick}, EnemyType: {scheduledEnemyData.enemyType}");
        enemies.Add(scheduledEnemyData);
    }

    private void SpawnEnemy(EnemyData enemyData)
    {
        GameObject enemyPrefab = null;
        switch (enemyData.enemyType)
        {
            case "RobotA":
                enemyPrefab = RobotAPrefab;
                break;
            default:
                Debug.LogWarning($"[EnemySpawner] Unknown enemy type: {enemyData.enemyType}");
                return;
                // Add more enemy types here
        }
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Init(enemyData.tick + prepareTick, enemyData.position);
        spawnedEnemies.Add(enemyController);
    }

    public void DestroyEnemy(float tick)
    {
        foreach (EnemyController enemy in spawnedEnemies)
        {
            if (enemy.targetTick == tick)
            {
                enemy.Die();
                spawnedEnemies.Remove(enemy);
            }
        }
    }
}
