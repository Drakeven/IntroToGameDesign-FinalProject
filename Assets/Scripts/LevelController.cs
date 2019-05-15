using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject[] players;
    public int[] playerScores;
    public GameObject[] playerUIScoreres;

    public float startDelay = 1f;
    public float repeatDelay = 1f;

    public int startTime = 10;
    public int currentTime;

    AudioSource myAudio;

    void Start()
    {
        //Debug.Log("Start!");

        players = GameObject.FindGameObjectsWithTag("Player");
        playerUIScoreres = GameObject.FindGameObjectsWithTag("Scorer UI");

        for (int i = 0; i < players.Length; i++)
        {
            playerUIScoreres[i].SendMessage("SetName", players[i].name);
        }

        currentTime = startTime;
        Time.timeScale = 1;

        myAudio = GetComponent<AudioSource>();
        myAudio.clip = Resources.Load<AudioClip>("Audio/teleport3");

        InvokeRepeating("SwapPlayers", startDelay, repeatDelay);
    }

    void Update()
    {

    }

    void IncreaseScore(string playerName)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == playerName)
            {

                playerUIScoreres[i].SendMessage("IncrementScore");
                
            }
        }
    }

    void SwapPlayers()
    {
        currentTime--;

        if (currentTime == 1)
        {
            myAudio.Play();
        }

        if (currentTime == 0)
        {
            //Debug.Log("SWAP!");

            if (players.Length == 2)
            {
                Vector3 tempPlayerLocation = players[0].transform.position;
                players[0].transform.position = players[1].transform.position;
                players[1].transform.position = tempPlayerLocation;
            }
            else if (players != null && players.Length > 1)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    // todo: make players not end up in the same position after all the swaps take place
                    // currently, it can swap from 1 -> 2, then 2 -> 3, then 3 -> 1
                    // this looks like you haven't moved at all, but you just moved back to your original position

                    int rnd = Random.Range(0, players.Length);
                    while (rnd == i)
                    {
                        rnd = Random.Range(0, players.Length);
                    }

                    //Debug.Log("player[" + rnd + "] to player[" + i + "]");
                    Vector3 tempPlayerLocation = players[rnd].transform.position;
                    players[rnd].transform.position = players[i].transform.position;
                    players[i].transform.position = tempPlayerLocation;
                }
            }
        }

        if (currentTime <= 0)
        {
            currentTime = startTime;
        }
    }
}