using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.Return;

    public string sceneName = "Level_1";

    public int counterMax = 3;
    public int currentCounter = 3;
    private bool counted = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (Input.GetKey(interactKey) && !counted)
            {
                InvokeRepeating("Countdown", 1f, 1f);
                counted = true;
            }
        }
    }
    
    void Countdown()
    {
        if (!counted)
        {
            currentCounter--;
        }
        
        if (currentCounter == 0)
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
