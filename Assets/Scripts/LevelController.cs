using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject[] players;
    public float startDelay = 10f;
    public float repeatDelay = 10f;

    void Start()
    {
        //Debug.Log("Start!");

        players = GameObject.FindGameObjectsWithTag("Player");
        //foreach (GameObject player in players)
        //{
        //    Debug.Log(player.name);
        //}

        Time.timeScale = 1;

        InvokeRepeating("SwapPlayers", startDelay, repeatDelay);
    }

    void SwapPlayers()
    {
        //Debug.Log("SWAP!");

        if (players != null && players.Length > 1)
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

                Debug.Log("player[" + rnd + "] to player[" + i + "]");
                Vector3 tempPlayerLocation = players[rnd].transform.position;
                players[rnd].transform.position = players[i].transform.position;
                players[i].transform.position = tempPlayerLocation;
            }
        }
    }
}