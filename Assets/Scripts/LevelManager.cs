using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public float respawnDelay;
    public PlayerScript gamePlayer;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
