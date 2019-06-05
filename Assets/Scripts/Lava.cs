using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    public GameObject player;
    public PlayerScript PlayerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player.SetActive(false);
        PlayerScript.Respawn();
        //player.SetActive(true);
    }

}
