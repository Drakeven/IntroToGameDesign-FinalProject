using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Objective that players must try and stand within
 * This adds score to the players who stand within it
 */
public class ObjectiveScorer : MonoBehaviour
{
    private LevelController levelController; // the Level Controller of the level

    void Start()
    {
        // find the Level Controller in the scene
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
    }

    public void AddScore(string scorerName)
    {
        // call the IncreaseScore method on the level controller
        levelController.SendMessage("IncreaseScore", scorerName);
    }

    public void CheckPlayerCollision(Collider2D collider)
    {
        // check if the provided collider's is a player
        if (collider.tag == "Player")
        {
            // call the AddScore method, passing the name of the player found
            AddScore(collider.name);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // check the collision of the collided object
        CheckPlayerCollision(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        // check the collision of the collided object
        CheckPlayerCollision(collider);
    }
}