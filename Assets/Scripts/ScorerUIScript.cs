using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorerUIScript : MonoBehaviour
{
    private TextMeshProUGUI scorerText;

    private LevelController levelController;
    // private PlayerScript playerScorring;
    public int startingScore = 0;
    private int score; // from LevelController
    private string playerName; // from LevelController

    void Start()
    {
        scorerText = GetComponent<TextMeshProUGUI>();
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();


        SetScore(startingScore);
        //score = playerScorring.getScore();
    }

    void Update()
    {
        scorerText.text = playerName + ": " + score.ToString();
        //playerScorring.SetScore(score);
    }


    void SetName(string newPlayerName)
    {
        playerName = newPlayerName;
    }

    void IncrementScore()
    {
        score++;

    }
    
    //void setRefrencedPlayer(PlayerScript player)
    //{

    //}
    
    
    void SetScore(int newScore)
    {
        score = newScore;
    }
}
