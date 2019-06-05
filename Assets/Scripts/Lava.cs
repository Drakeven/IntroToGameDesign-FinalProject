using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public PlayerScript PlayerScript1;
    public PlayerScript PlayerScript2;
    public LevelManager gameLevelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player1)
        {
            PlayerScript1.ResetPos();
        }
        else if (collision.gameObject == player2)
        {
            PlayerScript2.ResetPos();
        }
    }

}
