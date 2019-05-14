using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUIScript : MonoBehaviour
{
    public float startDelay = 0f;
    public float repeatDelay = 1f;

    private TextMeshProUGUI counterText;
    public string timeUntilText = "Time To Swap: ";
    private int startTime; // from LevelController
    private int currentTime; // from LevelController

    LevelController levelController;

    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        startTime = levelController.startTime;
        currentTime = levelController.currentTime;
        
        InvokeRepeating("UpdateTimer", startDelay, repeatDelay);
    }

    // Update is called once per frame
    void UpdateTimer()
    {
        currentTime = levelController.currentTime;
        counterText.text = timeUntilText + currentTime.ToString();
    }
}
