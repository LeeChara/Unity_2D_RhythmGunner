using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResultUIManager : MonoBehaviour
{
    ResultData resultData;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI perfectCountText;
    public TextMeshProUGUI goodCountText;
    public TextMeshProUGUI missCountText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    public Image gradeImage;
    public Image AFImage;
    public Image UBImage;
    public Image progressImage;

    public Sprite gradeS;
    public Sprite gradeA;
    public Sprite gradeB;
    public Sprite gradeC;
    public Sprite AFActive;
    public Sprite AFInactive;
    public Sprite UBActive;
    public Sprite UBInactive;

    private float progress;
    private float progressSum;
    private float progressLevel;

    private int score;
    private int scoreSum;
    private void Start()
    {
        resultData = DataCarrier.Instance.GetData();
        titleText.text = resultData.title;
        perfectCountText.text = resultData.perfectCount.ToString();
        goodCountText.text = resultData.goodCount.ToString();
        missCountText.text = resultData.missCount.ToString();
        comboText.text = resultData.maxCombo.ToString();

        progress = resultData.progress;
        progressSum = progress + resultData.progressChange;

        score = 0;
        scoreSum = resultData.score;

        switch (resultData.grade)
        {
            case "S":
                gradeImage.sprite = gradeS;
                break;
            case "A":
                gradeImage.sprite = gradeA;
                break;
            case "B":
                gradeImage.sprite = gradeB;
                break;
            case "C":
                gradeImage.sprite = gradeC;
                break;
        }

        if (resultData.isAllPerfect)
        {
            AFImage.sprite = AFActive;
        }
        else
        {
            AFImage.sprite = AFInactive;
        }

        if (resultData.isFullCombo)
        {
            UBImage.sprite = UBActive;
        }
        else
        {
            UBImage.sprite = UBInactive;
        }

    }

    private void Update()
    {
        if (progress < progressSum)
        {
            progress += Time.deltaTime * 70.0f;
        }
        if (progress > progressSum)
        {
            progress = progressSum;
        }
        if(progress >= 100)
        {
            progress -= 100;
            progressLevel += 1;
        }
        progressImage.rectTransform.offsetMax = new Vector2(- (540 * (1 - progress / 100f)), progressImage.rectTransform.offsetMax.y);

        if (score < scoreSum)
        {
            score = (int) ((float) score + Time.deltaTime * 1000000f);
        }
        if (score >= scoreSum)
        {
            score = scoreSum;
        }
        scoreText.text = score.ToString();
    }
}
