using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public KeyCode interactKey; //key to interact
    public GameObject ui; //on screen text
    public string level; //level to be changed to
    public GameObject player; //player

    void OnTriggerStay2D(Collider2D other) //while the player is in the trigger
    {
        ui.SetActive(true); //text is displayed 
        if (Input.GetKeyDown(interactKey)) //if player interacts
        {
            SceneManager.LoadScene(level); //change scene
        }
    }

    void OnTriggerExit2D(Collider2D collision) //when player leaves trigger
    {
        ui.SetActive(false); //text is hidden
    }
}
