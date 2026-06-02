using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

/// <summary>
/// JSON 문자열을 ChartData 객체로 파싱, Newtonsoft 패키지를 활용
/// </summary>
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
                        float posX = Random.Range(0.4f, 0.9f);
                        float posY = Random.Range(0.4f, 0.9f);
                        position = new Position {x = posX, y = posY};
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

                case "TextEffect":
                    string text = jo["text"]?.ToString() ?? "";
                    position = jo["position"]?.ToObject<Position>();
                    if (position == null) position = new Position { x = 0, y = 0 };

                    eventData = new TextEffectData
                    {
                        tick = jo["tick"].ToObject<float>(),
                        type = type,
                        text = text,
                        position = position,
                        size = jo["size"]?.ToObject<int>() ?? 24,
                        color = jo["color"]?.ToString() ?? "#FFFFFF",
                        align = jo["align"]?.ToString() ?? "Center",
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