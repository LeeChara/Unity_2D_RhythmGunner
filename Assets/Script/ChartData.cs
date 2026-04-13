[System.Serializable]
public class ChartData
{
    public MetaData metadata;
    public NoteData[] notes;
}

[System.Serializable]
public class MetaData
{
    public string title;
    public float bpm;
    public int resolution;
    public float offset;
}

[System.Serializable]
public class NoteData
{
    public int tick;
    public string type;
}

[System.Serializable]
public class RuntimeNote
{
    public int spawnTick;
    public string type;
}
