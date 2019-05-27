using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public GoToLevel [] GoToLevel; //array for each level's script

    public GameObject ui; //countdown ui

    [SerializeField] private Text uiText; //text of countdown
    [SerializeField] private float mainTimer; //initial time

    private float timer; //current time
    private bool stillRunning = false; //if the countdown is still running
    private bool isFinished = true; //if the countdown is complete

    private void Start()
    {
        ui.SetActive(false); //timer is hidder
        timer = mainTimer; //timer is set to initial time
    }

    void Update()
    {
        if (timer > 0.51f && stillRunning) //if the timer is running
        {
            timer -= Time.deltaTime; //countdown
            uiText.text = timer.ToString("N0"); //update timer text
        }

        else if (timer <= 0.51f && !isFinished) //if the timer is finished
        {
            stillRunning = false; 
            isFinished = true;
            uiText.text = "Good Luck!";
            timer = 0.0f;
            for (int i = 0; i < 4; i++) //tells specific section that countdown is complete
            {
                GoToLevel[i].countdownComplete();
            }
        }
    }

    public void TimerStart() //starts the timer
    {
        ui.SetActive(true);
        stillRunning = true;
        isFinished = false;
    }

    public void ResetTimer() //resets the timer
    {
        ui.SetActive(false);
        timer = mainTimer;
        uiText.text = timer.ToString("F");
        stillRunning = false;
        isFinished = false;
    }
}
