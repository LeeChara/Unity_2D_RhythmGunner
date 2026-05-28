using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 디버깅용 클래스. ChartData 객체의 내용을 콘솔에 출력하여 데이터가 올바르게 파싱되고 스케줄링되는지 확인
/// </summary>
public class ChartDataViewer : MonoBehaviour
{
    public void ViewChartData(ChartData chartData)
    {
        Debug.Log($"[ChartDataViewer] Title: {chartData.metaData.title}");
        Debug.Log($"[ChartDataViewer] BPM: {chartData.metaData.bpm}");
        Debug.Log($"[ChartDataViewer] Resolution: {chartData.metaData.resolution}");
        Debug.Log($"[ChartDataViewer] Offset: {chartData.metaData.offset} sec");

        foreach (EventData e in chartData.events)
        {
            switch (e.type)
            {
                case "Note":
                    NoteData note = e as NoteData;
                    Debug.Log($"[ChartDataViewer] Note - Tick: {note.tick}, NoteType: {note.noteType}, Intensity: {note.intensity}");
                    break;
                case "Enemy":
                    EnemyData enemy = e as EnemyData;
                    Debug.Log($"[ChartDataViewer] Enemy - Tick: {enemy.tick}, EnemyType: {enemy.enemyType}, Position: ({enemy.position.x}, {enemy.position.y})");
                    break;
                case "Boss":
                    BossData boss = e as BossData;
                    Debug.Log($"[ChartDataViewer] Boss - Tick: {boss.tick}, BossType: {boss.bossType}, Action: {boss.bossAction}");
                    break;
                case "TextEffect":
                    TextEffectData textEffect = e as TextEffectData;
                    Debug.Log($"[ChartDataViewer] TextEffect - Tick: {textEffect.tick}, Text: {textEffect.text}, Position: ({textEffect.position.x}, {textEffect.position.y}), Size: {textEffect.size}, Duration: {textEffect.duration} sec");
                    break;
                case "NoteEffect":
                    NoteEffectData noteEffect = e as NoteEffectData;
                    Debug.Log($"[ChartDataViewer] NoteEffect - Tick: {noteEffect.tick}, NoteType: {noteEffect.noteType}, StartPosition: ({noteEffect.startPosition.x}, {noteEffect.startPosition.y}), EndPosition: ({noteEffect.endPosition.x}, {noteEffect.endPosition.y}), Duration: {noteEffect.duration} sec");
                    break;
                case "AlertEffect":
                    AlertEffectData alertEffect = e as AlertEffectData;
                    Debug.Log($"[ChartDataViewer] AlertEffect - Tick: {alertEffect.tick}, AlertType: {alertEffect.alertType}");
                    break;
                case "BPMEvent":
                    BPMEventData bpmEvent = e as BPMEventData;
                    Debug.Log($"[ChartDataViewer] BPMEvent - Tick: {bpmEvent.tick}, New BPM: {bpmEvent.bpm}");
                    break;
            }       
        }
    }
}
