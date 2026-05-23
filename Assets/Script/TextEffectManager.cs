using UnityEngine;
using System.Collections.Generic;

public class TextEffectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject textEffectPrefab; // 텍스트 효과 프리팹

    [SerializeField]
    private List<TextEffectData> textEffects = new List<TextEffectData>();

    public RectTransform canvas;
    private void Update()
    {
        // 현재 Tick에 스폰될 텍스트 효과가 있으면 소환하고, 리스트에서 제거
        while (textEffects.Count > 0 && TickClock.Instance.Tick >= textEffects[0].tick)
        {
            SpawnTextEffect(textEffects[0], canvas);
            textEffects.RemoveAt(0);
        }
    }

    public void AddSchedule(TextEffectData textEffectData)
    {
        textEffects.Add(textEffectData);
    }

    private void SpawnTextEffect(TextEffectData textEffectData, RectTransform canvas)
    {
        GameObject textEffect = Instantiate(textEffectPrefab, Vector3.zero, Quaternion.identity, canvas);
        textEffect.GetComponent<TextEffectController>().Init(textEffectData, canvas);
    }
}
