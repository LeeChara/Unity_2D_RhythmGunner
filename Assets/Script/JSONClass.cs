using System.Collections.Generic;
public class ChartData // 기본 차트 데이터 클래스
{
    public MetaData metaData;
    public List<EventData> events;
}
public class Position // 위치 데이터 클래스
{
    public float x;
    public float y;
}
public class MetaData // 곡 정보 클래스
{
    public string title;
    public float bpm;
    public int resolution;
    public float offset; // sec, positive: delay music, negative: advance music
}
public class EventData // 이벤트 기본 클래스
{
    public float tick;
    public string type; // Type of event, e.g., "Note", "Enemy", "Boss" etc.
}
public class NoteData : EventData // 노트 클래스
{
    public NoteType noteType;
    public string intensity; // "Normal", "Strong", "Weak" 등으로 구분
}
public class EnemyData : EventData // 적 클래스
{
    public string enemyType;
    public Position position;
}
public class BossData : EventData // 보스 클래스
{
    public string bossType;
    public string bossAction; // "Appear", "Disappear" 및 기타 보스 행동
}

public class TextEffectData : EventData // 텍스트 효과 클래스
{
    public string text;
    public Position position;
    public int size;
    public float duration;
}

public class NoteEffectData : EventData // 노트 효과 클래스. 노트 클래스와 다르게 판정 기능이 없는 이펙트
{
    public string noteType;
    public Position startPosition;
    public Position endPosition; // if equal to startPosition, its velocity is zero
    public float duration;
}

public class AlertEffectData : EventData // 경고 효과 클래스
{
    public string alertType;
}

public class BPMEventData : EventData // BPM 변경 클래스, 노트 이동속도에 영향을 줌
{
    public float bpm; // notespeed will be changed if bpm changes
}

public class SVEventData : EventData // Scroll Velocity 변경 클래스, bpm은 변화하지 않으면서 노트 이동속도에 영향을 줌
{
    public float sv; // scroll velocity multiplier
}