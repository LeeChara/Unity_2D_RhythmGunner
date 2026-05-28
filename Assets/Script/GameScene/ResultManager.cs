using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class ResultManager : MonoBehaviour
{
    public ResultData resultData;
    private int noteNumber;
    private float progress;
    public void Init(int noteNumber)
    {
        resultData = new ResultData();
        this.noteNumber = noteNumber;
    }

    public void SetTitle(string title)
    {
        resultData.title = title;
    }
    public void OnPerfect()
    {
        resultData.perfectCount++;
    }
    public void OnGood()
    {
        resultData.goodCount++;
    }
    public void OnMiss()
    {
        resultData.missCount++;
    }
    public void SetScore(int score)
    {
        resultData.score = score;
    }
    public void UpdateMaxCombo(int combo)
    {
        if (resultData.maxCombo < combo) resultData.maxCombo = combo;
    }
    public void SetProgress(float progress)
    {
        this.progress = progress;
    }
    public ResultData GetResultData()
    {
        Debug.Log($"[ResultManager] maxNotes : {this.noteNumber}");
        if (resultData.perfectCount == this.noteNumber)
        {
            resultData.isAllPerfect = true;
        }
        else
        {
            resultData.isAllPerfect = false;
        }
        if (resultData.maxCombo == this.noteNumber)
        {
            resultData.isFullCombo = true;
        }
        else
        {
            resultData.isFullCombo = false;
        }

            float ratio = (float)resultData.score / 1000000;
        if (ratio >= 0.9f) resultData.grade = "S";
        else if (ratio >= 0.8f) resultData.grade = "A";
        else if (ratio >= 0.6f) resultData.grade = "B";
        else resultData.grade = "C";

        resultData.progressChange = resultData.score / 10000.0f * 0.9f;
        if (resultData.isAllPerfect)
        {
            resultData.progressChange *= 1.1f;
        }
        else
        {
            resultData.progressChange *= 1.05f;
        }
        Debug.Log($"[ResultManager] Result Data perfectCount : {resultData.perfectCount} , goodCount : {resultData.goodCount} ,missCount : {resultData.missCount} ,score : {resultData.score} ,grade : {resultData.grade} ,maxCombo : {resultData.maxCombo} ,isFullCombo : {resultData.isFullCombo} ,isAllPerfect : {resultData.isAllPerfect} ,progress : {resultData.progress} ,progressChange : {resultData.progressChange}");
        return resultData;
    }
}
