using System.Collections.Generic;
public class ChartData
{
    public MetaData metaData;
    public List<EventData> events;
}
public class Position
{
    public float x;
    public float y;
}
public class MetaData
{
    public string title;
    public float bpm;
    public int resolution;
    public float offset; // sec, positive: delay music, negative: advance music
}
public class EventData
{
    public float tick;
    public string type; // Type of event, e.g., "Note", "Enemy", "Boss" etc.
}
public class NoteData : EventData
{
    public NoteType noteType;
    public string intensity; // "Normal", "Strong", "Weak" 등으로 구분
}
public class EnemyData : EventData
{
    public string enemyType;
    public Position position;
}
public class BossData : EventData
{
    public string bossType;
    public string bossAction; // "Appear", "Disappear" etc.
}

public class BossAttackData : EventData
{
    public string bossType;
    public string attackType;
}

public class TextEffectData : EventData
{
    public string text;
    public Position position;
    public int size;
    public float duration;
}

public class NoteEffectData : EventData
{
    public string noteType;
    public Position startPosition;
    public Position endPosition; // if equal to startPosition, its velocity is zero
    public float duration;
}

public class AlertEffectData : EventData
{
    public string alertType;
}

public class BPMEventData : EventData
{
    public float bpm; // notespeed will be changed if bpm changes
}

public class SVEventData : EventData
{
    public float sv; // scroll velocity multiplier
}