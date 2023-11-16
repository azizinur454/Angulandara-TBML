using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score instance;

    public TMP_Text scoreText;
    public TMP_Text completionScoreText;

    private int currentScore = 0;
    private int currentStageScore = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = "Score : " + currentScore;
    }
    public void AddCoinScore(int addScore)
    {
        currentScore += addScore;
        scoreText.text = "Score : " + currentScore;
    }

    public void AddKillingEnemyScore(int addScore)
    {
        currentScore += addScore;
        scoreText.text = "Score : " + currentScore;
    }

    public void CompletionScoreStage()
    {
        currentStageScore = currentScore;
        completionScoreText.text = "Score : " + currentStageScore;
    }
}
