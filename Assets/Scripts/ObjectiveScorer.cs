using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScorer : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {

    }

    void AddScore(string scorerName)
    {
        //this debug log creates LOTS of lag :/
        //Debug.Log(scorerName + " is in the objective zone!");

        // we need to get the player gameobject, and add score to it
        // from there, we need to display that score in the UI somehow
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            AddScore(collider.name);
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            AddScore(collider.name);
        }
    }
}
