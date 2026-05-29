using UnityEngine;

[CreateAssetMenu(menuName = "RhythmGunner/Song Data")]
public class SongData : ScriptableObject
{
    [Header("Song Info")]
    public string songTitle;

    public string artist;

    public int BPM;

    [TextArea]
    public string description;

    [Header("Difficulty")]
    public int easyLevel;
    public int normalLevel;
    public int hardLevel;

    [Header("Assets")]
    public Sprite jacketImage;

    public AudioClip previewClip;

    [Header("Scene")]
    public string playSceneName;
}