using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * The Scorer UI GameObject, which displays a player's current score
 */
public class ScorerUIScript : MonoBehaviour
{
    private TextMeshProUGUI scorerText; // the text Component to update 
    private PlayerScript playerRef; // the player reference to get a score from
    private string playerName; // the player's name - this is saved to reduce how often we access data from the player

    void Start()
    {
        // get the scorer text Component off this GameObject
        scorerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        // check that a player has actually been referenced, otherwise there isn't a score to show
        if (playerRef != null)
        {
            scorerText.text = playerName + ": " + playerRef.GetScore().ToString();
        }
    }

    void SetPlayerRef(PlayerScript player)
    {
        // set the reference object
        playerRef = player;

        // set the player name
        playerName = playerRef.name;
    }
}