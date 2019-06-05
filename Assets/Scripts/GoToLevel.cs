using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public Countdown Countdown;

    public KeyCode interactKey; //key to interact
    public KeyCode cancelKey; //filler key until cancel with same interact key is figured out!!!
    public GameObject ui; //on screen text
    public string level; //level to be changed to
    public GameObject player; //player

    private bool countdownFinished = false; //if countdown is finished or not
    private bool isCounting = false; //if the countdown is active

    void OnTriggerStay2D(Collider2D other) //while the player is in the trigger
    {
        ui.SetActive(true); //text is displayed 
        if (Input.GetKeyDown(interactKey)) //if player interacts
        { 
            if (!isCounting) //if the timer has not started
            {
                //Countdown.ResetTimer();
                //isCounting = false;
                Countdown.TimerStart(); //start timer
                isCounting = true;
            }
            //else
            //{
                //Countdown.TimerStart(); //start timer
                //isCounting = true;  
                //Countdown.ResetTimer();
                //isCounting = false;
            //}
        }

        if (Input.GetKeyDown(cancelKey) && isCounting) //filler until else statement is fixed
        {
            Countdown.ResetTimer();
            isCounting = false;
        }

        if (countdownFinished) //if the countdown reaches 0
        {
            SceneManager.LoadScene(level); //change scene
        }
    }

    void OnTriggerExit2D(Collider2D collision) //when player leaves trigger
    {
        ui.SetActive(false); //text is hidden
        isCounting = false; 
        Countdown.ResetTimer(); //resets the timer
    }

    public void countdownComplete() //called by countdown script
    {
        countdownFinished = true; //countdown is finished
    }
}
