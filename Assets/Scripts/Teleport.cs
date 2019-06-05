using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //public KeyCode interactKey;
    //public GameObject ui;
    public Transform teleportLocation;
    public GameObject player1;
    public GameObject player2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player1)
        {
            player1.transform.position = teleportLocation.transform.position;
        }
        else if(other.gameObject == player2)
        {
            player2.transform.position = teleportLocation.transform.position;
        }
        //ui.SetActive(true);
        //if (Input.GetKeyDown(interactKey))
        //{
            //player.transform.position = teleportLocation.transform.position; //player position changes to teleportLocation positions
        //}
    }

    //void OnTriggerExit2D(Collider2D collision)
    //{
        //ui.SetActive(false);
    //}
}
