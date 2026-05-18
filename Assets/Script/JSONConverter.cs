using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class JSONConverter : MonoBehaviour
{
    public ChartData Load(string jsonString)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Chart/" + jsonString);
        string jsonContent = jsonFile.text;

        JObject jsonObject = JObject.Parse(jsonContent);

        ChartData chartData = new ChartData();
        chartData.metaData = jsonObject["metaData"].ToObject<MetaData>();
        chartData.events = new List<EventData>();

        foreach (JObject jo in jsonObject["events"])
        {
            string type = jo["type"].ToString();
            EventData eventData = null;

            switch (type)
            {
                case "Note":
                    string noteTypeStr = jo["noteType"]?.ToString() ?? "Attack";
                    if (jo["noteType"] == null)
                        Debug.LogWarning($"[JSONConverter] Note event at tick {jo["tick"]} is missing 'noteType'. Defaulting to 'Attack'.");
                    if (!System.Enum.TryParse<NoteType>(noteTypeStr, out NoteType noteType))
                    {
                        Debug.LogWarning($"[JSONConverter] Note event at tick {jo["tick"]} has invalid 'noteType' value '{noteTypeStr}'. Defaulting to 'Attack'.");
                        noteType = NoteType.Attack;
                    }

                    eventData = new NoteData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        noteType = noteType,
                        intensity = jo["intensity"]?.ToString() ?? "normal"
                    };
                    break;

                case "Enemy":
                    string enemyType = jo["enemyType"]?.ToString() ?? "Unknown";
                    if (jo["enemyType"] == null)
                        Debug.LogWarning($"[JSONConverter] Enemy event at tick {jo["tick"]} is missing 'enemyType'. Defaulting to 'Unknown'.");
                    Position position = jo["position"]?.ToObject<Position>();
                    if (position == null)
                    {
                        // Temporarily set to (0,0) if position is missing.
                        position = new Position { x = 0, y = 0 };
                    }

                    eventData = new EnemyData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        enemyType = enemyType,
                        position = position
                    };
                    break;

                case "Boss":
                    string bossType = jo["bossType"]?.ToString() ?? "Unknown";
                    if (jo["bossType"] == null)
                        Debug.LogWarning($"[JSONConverter] Boss event at tick {jo["tick"]} is missing 'bossType'. Defaulting to 'Unknown'.");
                    string bossAction = jo["bossAction"]?.ToString() ?? "Unknown";
                    if (jo["bossAction"] == null)
                        Debug.LogWarning($"[JSONConverter] Boss event at tick {jo["tick"]} is missing 'bossAction'. Defaulting to 'Unknown'.");

                    eventData = new BossData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        bossType = bossType,
                        bossAction = bossAction
                    };
                    break;

                case "BossAttack":
                    bossType = jo["bossType"]?.ToString() ?? "Unknown";
                    if (jo["bossType"] == null)
                        Debug.LogWarning($"[JSONConverter] BossAttack event at tick {jo["tick"]} is missing 'bossType'. Defaulting to 'Unknown'.");
                    string attackType = jo["attackType"]?.ToString() ?? "Unknown";
                    if (jo["attackType"] == null)
                        Debug.LogWarning($"[JSONConverter] BossAttack event at tick {jo["tick"]} is missing 'attackType'. Defaulting to 'Unknown'.");

                    eventData = new BossAttackData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        bossType = bossType,
                        attackType = attackType
                    };
                    break;

                case "TextEffect":
                    string text = jo["text"]?.ToString() ?? "";
                    position = jo["position"]?.ToObject<Position>();
                    if (position == null)
                    {
                        // Temporarily set to (0,0) if position is missing.
                        position = new Position { x = 0, y = 0 };
                    }
                    eventData = new TextEffectData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        text = text,
                        position = position,
                        size = jo["size"]?.ToObject<int>() ?? 24,
                        duration = jo["duration"]?.ToObject<float>() ?? 1.0f,
                    };
                    break;

                case "NoteEffect":
                    noteTypeStr = jo["noteType"]?.ToString() ?? "Attack";
                    if (jo["noteType"] == null)
                            Debug.LogWarning($"[JSONConverter] NoteEffect event at tick {jo["tick"]} is missing 'noteType'. Defaulting to 'Attack'.");
                    if (!System.Enum.TryParse<NoteType>(noteTypeStr, out noteType))
                    {
                        Debug.LogWarning($"[JSONConverter] NoteEffect event at tick {jo["tick"]} has invalid 'noteType' value '{noteTypeStr}'. Defaulting to 'Attack'.");
                        noteType = NoteType.Attack;
                    }
                        Position startPosition = jo["startPosition"]?.ToObject<Position>();
                    if (startPosition == null)
                    {
                        // Temporarily set to (0,0) if startPosition is missing.
                        startPosition = new Position { x = 0, y = 0 };
                    }

                    eventData = new NoteEffectData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        noteType = jo["noteType"]?.ToString() ?? "Attack",
                        startPosition = startPosition,
                        endPosition = jo["endPosition"]?.ToObject<Position>() ?? startPosition,
                        duration = jo["duration"]?.ToObject<float>() ?? 1.0f,
                    };
                    break;

                case "AlertEffect":
                    string alertEffect = jo["alertType"]?.ToString() ?? "Unknown";
                    if (jo["alertType"] == null)
                        Debug.LogWarning($"[JSONConverter] AlterEvent at tick {jo["tick"]} is missing 'alertType'. Defaulting to 'Unknown'.");

                    eventData = new AlertEffectData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        alertType = alertEffect
                    };
                    break;

                case "BPMEvent":
                    float bpm = jo["bpm"]?.ToObject<float>() ?? 120f;
                    if (jo["bpm"] == null)
                        Debug.LogWarning($"[JSONConverter] BPMEvent at tick {jo["tick"]} is missing 'bpm'. Defaulting to 120.");

                    eventData = new BPMEventData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        bpm = bpm
                    };
                    break;

                case "SVEvent":
                    float sv = jo["sv"]?.ToObject<float>() ?? 1f;
                    if (jo["sv"] == null)
                        Debug.LogWarning($"[JSONConverter] SVEvent at tick {jo["tick"]} is missing 'sv'. Defaulting to 1.");

                    eventData = new SVEventData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        sv = sv
                    };
                    break;

                default:
                    Debug.LogWarning($"[JSONConverter] Unknown event type '{type}' at tick {jo["tick"]}. Skipping this event.");
                    break;
            }
            if (eventData != null)
                chartData.events.Add(eventData);
        }
        return chartData;
    }
}