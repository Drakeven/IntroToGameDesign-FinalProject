using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScorer : MonoBehaviour
{
    private LevelController levelController;

    void Start()
    {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
    }
    
    void Update()
    {

    }

    void AddScore(string scorerName)
    {
        levelController.SendMessage("IncreaseScore", scorerName);
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
