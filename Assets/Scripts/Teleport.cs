using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public KeyCode interactKey;
    public GameObject ui;
    public Transform teleportLocation;
    public GameObject player;

    void OnTriggerStay2D(Collider2D other)
    {
        ui.SetActive(true);
        if (Input.GetKeyDown(interactKey))
        {
            player.transform.position = teleportLocation.transform.position; //player position changes to teleportLocation positions
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ui.SetActive(false);
    }
}
