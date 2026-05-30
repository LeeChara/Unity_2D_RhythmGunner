using System.Collections.Generic;
using UnityEngine;

public class GameTester : MonoBehaviour
{
    public bool activateJump;
    public float jumpTick;

    public BPMEventManager bpmEventManager;
    public void Jump()
    {
        if (!activateJump) return;

        float musicTime = 0f;
        float previousTick = 0f;
        float previousBpm = GameManager.Instance.bpm;
        List<BPMEventData> bpmEvents = bpmEventManager.GetEvents();

        foreach (BPMEventData bpmEvent in bpmEvents)
        {
            if(bpmEvent.tick > jumpTick)
                break;
            musicTime += (bpmEvent.tick - previousTick) / (previousBpm / 60f * GameManager.Instance.resolution);
            previousTick = bpmEvent.tick;
            previousBpm = bpmEvent.bpm;
        }
        musicTime += (jumpTick - previousTick) / (previousBpm / 60f * GameManager.Instance.resolution);

        Debug.Log($"[GameTester] Jumping to tick: {jumpTick}, calculated musicTime: {musicTime}, bpm: {previousBpm}");

        GameManager.Instance.musicPlayer.JumpTo(musicTime);
        TickClock.Instance.JumpTo(jumpTick, previousBpm);

        GameManager.Instance.noteManager.SkipEvent(jumpTick);
        GameManager.Instance.enemyManager.SkipEvent(jumpTick);
        GameManager.Instance.bossManager.SkipEvent(jumpTick);
        GameManager.Instance.textEffectManager.SkipEvent(jumpTick);
        GameManager.Instance.noteEffectManager.SkipEvent(jumpTick);
        GameManager.Instance.alertEffectManager.SkipEvent(jumpTick);
        GameManager.Instance.bpmEventManager.SkipEvent(jumpTick);
    }
}
