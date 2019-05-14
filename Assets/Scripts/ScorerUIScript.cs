using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorerUIScript : MonoBehaviour
{
    private TextMeshProUGUI scorerText;

    private LevelController levelController;

    public int startingScore = 0;
    private int score; // from LevelController
    private string playerName; // from LevelController

    void Start()
    {
        scorerText = GetComponent<TextMeshProUGUI>();
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();

        score = startingScore;
    }

    void Update()
    {
        scorerText.text = playerName + ": " + score.ToString();
    }

    void SetName(string newPlayerName)
    {
        playerName = newPlayerName;
    }

    void IncrementScore()
    {
        score++;
    }
    
    void SetScore(int newScore)
    {
        score = newScore;
    }
}
