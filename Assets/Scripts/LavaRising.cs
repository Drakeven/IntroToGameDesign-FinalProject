﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRising : MonoBehaviour
{

    public float speed; //speed of rising lava
    public GameObject player; //player

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0); //lava rises
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player) //if the player touches the lava
        {
            player.SetActive(false); //player dies
        }
    }
}
