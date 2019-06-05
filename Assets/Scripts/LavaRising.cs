using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRising : MonoBehaviour
{

    public Transform lava;
    public Transform startingPos;
    public Transform lowRise;
    public Transform highRise;
    public Vector3 newPos;
    public string currentState;
    public float smooth;
    public float resetTime;
    public GameObject player; //player

    private void Start()
    {
        ChangePos();
    }

    private void FixedUpdate()
    {
        lava.position = Vector3.Lerp(lava.position, newPos, smooth * Time.deltaTime);
    }

    void ChangePos()
    {
        if(currentState == "ToStartPos")
        {
            currentState = RandomRise();
            if(currentState == "ToLowRise")
            {
                newPos = lowRise.position;
            }
            else if(currentState == "ToHighRise")
            {
                newPos = highRise.position;
            }
        }
        else if(currentState == "ToLowRise" || currentState == "ToHighRise")
        {
            currentState = "ToStartPos";
            newPos = startingPos.position;
        }
        else if(currentState == "")
        {
            currentState = "ToLowRise";
            newPos = lowRise.position;
        }

        Invoke("ChangePos", resetTime);
    }

    string RandomRise()
    {
        bool setRise = (Random.value > 0.5f);
        if (setRise) return "ToLowRise";
        else return "ToHighRise";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered"); 
        player.SetActive(false);
    }

}
