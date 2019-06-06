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
    public Vector3 spawnPos;
    public Vector3 spawnPos2;
    public Vector3 spawnPos3;
    public Vector3 spawnPos4;

    public int timer = 40000;
    public int currentTime = 40000;

    public int scoreCountTimer = 0;
    public int scoreCountTimerMax = 120;

    void Start()
    {
        // find the Level Controller in the scene
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
    }

    void FixedUpdate()
    {
        timerCount();
        objPos();

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
            if (scoreCountTimer >= scoreCountTimerMax)
            {
                //Debug.Log(collider.name);
                // call the AddScore method, passing the name of the player found
                AddScore(collider.name);
                scoreCountTimer = 0;
            }
            scoreCountTimer++;
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

    void timerCount()
    {
        currentTime--;
        if (currentTime <= 0)
        {
            currentTime = timer;
        }
    }

    void objPos()
    {
        if (currentTime >= (timer / 2 + timer / 4))
        {
            transform.position = spawnPos;
        }
        if (currentTime >= timer / 2 && currentTime < (timer / 2 + timer / 4))
        {
            transform.position = spawnPos2;
        }
        if (currentTime >= timer / 4 && currentTime < timer / 2)
        {
            transform.position = spawnPos3;
        }
        if (currentTime < timer / 4)
        {
            transform.position = spawnPos4;
        }
    }
}