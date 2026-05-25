using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    private int score;
    private int displayScore; // 화면에 표시되는 점수. 실제 score에 점차적으로 접근
    private int point; // 노트 하나당 점수
    private int combo;

    public RectTransform scoreText;
    public RectTransform comboText;
    public RectTransform titleText;

    public float goodScoreMultiplier = 0.6f; // Good 판정 시 점수 배율
    private void Update()
    {
        if(displayScore < score)
        {
            displayScore += Mathf.CeilToInt((score - displayScore) * Time.deltaTime * 10f);
            if(displayScore > score)
                displayScore = score;
        }

        scoreText.GetComponent<TextMeshProUGUI>().text = displayScore.ToString("D7");
        comboText.GetComponent<TextMeshProUGUI>().text = combo.ToString();
    }
    public void Init(int noteNumber, MetaData metaData)
    {
        score = 0;
        displayScore = 0;
        combo = 0;
        point = 1000000 / noteNumber; // 노트 하나당 점수

        titleText.GetComponent<TextMeshProUGUI>().text = metaData.title;
    }

    public void OnPerfect()
    {
        score = Mathf.CeilToInt(score + point);
        if (score > 1000000) score = 1000000; // 최대 점수는 100만점
        combo++;
    }
    public void OnGood()
    {
        score = Mathf.CeilToInt(score + point * goodScoreMultiplier);
        if (score > 1000000) score = 1000000; // 최대 점수는 100만점
        combo++;
    }
    public void OnMiss()
    {
        combo = 0;
    }
}
