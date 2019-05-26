using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollider : MonoBehaviour
{

    private BoxCollider2D myCollider; //box collider variable

    public GameObject player; //player

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>(); //gets the box collider of the object
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > gameObject.transform.position.y + 1.5) //if the player's position on the y axis is greater than the collider's position
        {
            myCollider.enabled = true; //collider is enabled
        }
        else //if the player's position on the y axis is lower than the collider's position
        {
            myCollider.enabled = false; //collider is disabled
        }
    }
}
