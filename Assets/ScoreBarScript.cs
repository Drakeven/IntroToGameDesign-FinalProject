using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarScript : MonoBehaviour
{
    public Image scoreBar;

    private PlayerScript playerRef; // the player reference to get a score from
    private int winScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreBar = GetComponent<Image>();
    }

    void SetPlayerRef(PlayerScript player)
    {
        playerRef = player;
        Debug.Log(player.name);
    }

    void SetMaxScore(int maxScore)
    {
        winScore = maxScore;
    }

    void Update()
    {
        UpdateScoreBar();
    }

    public void UpdateScoreBar()
    {
        scoreBar.fillAmount = (float) playerRef.GetScore() / (float) winScore;
    }
}
