using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class BossManager : MonoBehaviour
{
    // 보스 프리팹
    [SerializeField]
    private BossController robotAlpha;

    private List<BossData> bossEvents = new List<BossData>(); // ChartScheduler로부터 받은 스케줄링된 보스 이벤트 리스트

    [SerializeField]
    private List<BossController> spawnedBoss = new List<BossController>();

    private void Update()
    {
        // 현재 Tick에 스폰될 보스 이벤트가 있으면 소환하고, 리스트에서 제거
        while (bossEvents.Count > 0 && TickClock.Instance.Tick >= bossEvents[0].tick)
        {
            ExecuteBossEvent(bossEvents[0]);
            bossEvents.RemoveAt(0);
        } 
    }

    // 보스 이벤트 추가 메서드. ChartScheduler에서 호출됨
    public void AddSchedule(BossData bossData)
    {
        // Debug.Log($"[BossManager] Scheduled Boss Event - Tick: {scheduledBossData.tick}, BossType: {scheduledBossData.bossType}");
        bossEvents.Add(bossData);
    }

    // 보스 이벤트 실행 메서드
    private void ExecuteBossEvent(BossData bossData)
    {
        if (bossData.bossAction == "Appear") // 보스 등장 이벤트
        {
            BossController bossPrefab = null;
            switch (bossData.bossType)
            {
                case "RobotAlpha":
                    bossPrefab = robotAlpha;
                    break;
                default:
                    Debug.LogWarning($"[BossManager] Unknown boss type: {bossData.bossType}");
                    return;
            }
            BossController boss = Instantiate(bossPrefab);
            boss.Execute("Appear"); // 보스 등장 이벤트 호출
            spawnedBoss.Add(boss);
            return;
        }
        if (bossData.bossAction == "Disappear") // 보스 퇴장 이벤트
        {
            foreach (BossController boss in spawnedBoss)
            {
                if (boss.BossType == bossData.bossType)
                {
                    boss.Execute("Disappear");
                    spawnedBoss.Remove(boss);
                    return;
                }
            }
            Debug.Log($"[BossManager] Boss doesn't exist: {bossData.bossType}"); // 소환된 보스가 존재하지 않음
        }

        foreach (BossController boss in spawnedBoss)
        {
            if (boss.BossType == bossData.bossType)
            {
                boss.Execute(bossData.bossAction); // 보스 액션 이벤트 호출
                return;
            }
        }
        Debug.Log($"[BossManager] Boss must Appear first: {bossData.bossType}");
    }
}