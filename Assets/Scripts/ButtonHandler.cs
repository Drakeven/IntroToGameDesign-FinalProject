using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public void LevelSelect()
    {
        SceneManager.LoadScene("levelSelect");
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void LevelFour()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit(); //Closes program
    }

    public void Back()
    {
        SceneManager.LoadScene("mainMenu");
    }

}
