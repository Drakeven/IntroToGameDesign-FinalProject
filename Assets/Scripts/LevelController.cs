using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * The Level Controller of a level
 * This holds and connects the core functionality of all levels
 */
public class LevelController : MonoBehaviour
{
    public GameObject[] players; // an array of all the players in the scene
    public GameObject[] playerUIScoreres; // an array of all the scorer UI's in the scene
    public GameObject[] playerUIScoreBars; // an array of all the scorer UI's in the scene

    public GameObject coin;

    public int startTime = 7; // the starting time to count down from, until swapping players
    public int currentTime; // the current time until swapping players

    public int winningScore = 240;

    public float startDelay = 1f; // the time until the SwapPlayers method starts running
    public float repeatDelay = 1f; // the time between the SwapPlayers method being called

    private AudioSource myAudio; // the AudioSource Component to play Audio from

    public bool isSwapping = true;

    public PlayerScript gamePlayer1;
    public PlayerScript gamePlayer2;
    public Vector3 spawnPos1;
    public Vector3 spawnPos2;

    void Start()
    {
        // get all the players in the scene at start
        players = GameObject.FindGameObjectsWithTag("Player");

        coin = GameObject.FindGameObjectWithTag("Coin");

        // get all the player UI scorers in the scene at start
        playerUIScoreres = GameObject.FindGameObjectsWithTag("Scorer UI");

        // get all the player Score Bar UI objects at start
        playerUIScoreBars = GameObject.FindGameObjectsWithTag("Score Bar UI");

        // check to see if there are the same number of players in scorers
        // a potential problem could be where we add the wrong number of one at startup
        if (players.Length == playerUIScoreres.Length)
        {
            // loop through all of the player GameObjects
            for (int i = 0; i < players.Length; i++)
            {
                // set the player reference on each of the scorer UI's
                playerUIScoreres[i].SendMessage("SetPlayerRef", players[i].GetComponent<PlayerScript>());
                playerUIScoreBars[i].SendMessage("SetPlayerRef", players[i].GetComponent<PlayerScript>());
                playerUIScoreBars[i].SendMessage("SetMaxScore", winningScore);
            }
        }

        // set the current time of the timer to the starting time
        currentTime = startTime;

        // set the time scale of our game to 1, to make sure there isn't any problems with our timer
        Time.timeScale = 1;

        // get the AudioSource Component on this GameObject
        myAudio = GetComponent<AudioSource>();

        // set the current AudioClip to play
        myAudio.clip = Resources.Load<AudioClip>("Audio/space_teleport");

        // call the SwapPlayers function after startDelay, and repeatingly call it every repeatDelay seconds
        InvokeRepeating("SwapPlayers", startDelay, repeatDelay);
    }

    //void coinSwap()
    //{
    //    Coinscript coinRef = coin.GetComponent<Coinscript>();
    //    if (coinRef.getcoinUp() == true)
    //    {
    //        innitiateSwap();
    //        coinRef.setcoinUp(false);
    //    }
    //}

    void IncreaseScore(string playerName)
    {
        // check if there are actually players in the scene
        if (players.Length > 0)
        {
            // loop through all of the player GameObjects
            for (int i = 0; i < players.Length; i++)
            {
                // check if the playerName provided is in the players array
                if (players[i].name == playerName)
                {
                    // call the IncrementScore method on the player found
                    //players[i].SendMessage("IncrementScore");
                    PlayerScript playerRef = players[i].GetComponent<PlayerScript>();
                    if (playerRef.GetScore() <= winningScore)
                    {
                        playerRef.IncrementScore();
                    }
                    else
                    {
                        Time.timeScale = 0;
                        StartCoroutine(delay());
                    }
                }
            }
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene("You Win!");
    }

    void SwapPlayers()
    {
        if (!isSwapping)
        {
            return;
        }

        // decrement the current time
        currentTime--;

        // if there is one second left on the timer
        if (currentTime == 1)
        {
            // play the current sound
            myAudio.Play();
        }

        // if there are no seconds left on the timer, and there are players in the scene
        if (currentTime == 0 && players != null)
        {
            InitiateSwap();
        }

        // if the current time is under the minimum time
        if (currentTime <= 0)
        {
            // set the current time to that of the start time's, restarting the countdown
            currentTime = startTime;
        }
    }

    public int GetCurrentTime()
    {
        // return the current time of the timer
        return currentTime;
    }

    public void InitiateSwap()
    {
        // todo: this can probably be optimised into just one if statement, so we don't check for 2 players

        // if there are only two players
        if (players.Length == 2)
        {
            // get the location of the first player
            Vector3 tempPlayerLocation = players[0].transform.position;
            //Vector2 tempPlayerVelocity = players[0].GetComponent<Rigidbody2D>().velocity;

            // set the location of the first player to that of the second's
            players[0].transform.position = players[1].transform.position;
            //players[0].GetComponent<Rigidbody2D>().velocity = players[1].GetComponent<Rigidbody2D>().velocity;

            // set the second player's location to that of the first player's
            players[1].transform.position = tempPlayerLocation;
            //players[1].GetComponent<Rigidbody2D>().velocity = tempPlayerVelocity;
        }
        // else if there are more than one players in the scene
        else if (players.Length > 1)
        {
            // loop through all of the player GameObjects
            for (int i = 0; i < players.Length; i++)
            {
                // todo: make players not end up in the same position after all the swaps take place
                // currently, it can swap from 1 -> 2, then 2 -> 3, then 3 -> 1
                // this looks like you haven't moved at all, but you just moved back to your original position
                // this isn't a problem while we are only testing with 2 players


                // get a random index within the range of total number of players
                int rnd = Random.Range(0, players.Length);

                // get another random index if the random number was the same as the index of the current player
                while (rnd == i)
                {
                    rnd = Random.Range(0, players.Length);
                }

                // get the location of the random player
                Vector3 tempPlayerLocation = players[rnd].transform.position;
                //Vector2 tempPlayerVelocity = players[rnd].GetComponent<Rigidbody2D>().velocity;

                // set the location of the random player to that of the current's
                players[rnd].transform.position = players[i].transform.position;
                //players[rnd].GetComponent<Rigidbody2D>().velocity = players[i].GetComponent<Rigidbody2D>().velocity;

                // set the current player's location to that of the random's
                players[i].transform.position = tempPlayerLocation;
                //players[i].GetComponent<Rigidbody2D>().velocity = tempPlayerVelocity;
            }
        }
    }

    public void Respawn()
    {
        StartCoroutine(ResetCoroutine());
    }

    IEnumerator ResetCoroutine()
    {
        gamePlayer1.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(3);
        gamePlayer1.transform.position = spawnPos1;
        gamePlayer1.gameObject.SetActive(true);
    }

}
