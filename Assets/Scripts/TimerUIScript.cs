using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * The Timer UI GameObject, which displays the time until player swap
 */
public class TimerUIScript : MonoBehaviour
{
    private TextMeshProUGUI counterText; // the text Component to update
    LevelController levelController; // the Level Controller of the level

    public string timeUntilText = "Time To Swap: "; // the base text of the counter

    public float startDelay = 0f; // delay until the update timer starts being called
    public float repeatDelay = 1f; // delay between updating the text

    void Start()
    {
        // get the counter text Component off this GameObject
        counterText = GetComponent<TextMeshProUGUI>();

        // find the Level Controller in the scene
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();

        // call the UpdateTimer function after startDelay, and repeatingly call it every repeatDelay seconds
        InvokeRepeating("UpdateTimer", startDelay, repeatDelay);
    }

    void UpdateTimer()
    {
        // check that the level controller has been set (meaning that it exists in the current scene)
        if (levelController != null)
        {
            // set the text to the base text and current time from the Level Controller
            counterText.text = timeUntilText + levelController.GetCurrentTime().ToString();
        }
    }
    }