using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public void LevelHub()
    {
        SceneManager.LoadScene("Level_Hub-Tutorial");
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void LevelFour()
    {
        SceneManager.LoadScene("Level_4");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit(); //Closes program
    }

    public void Back()
    {
        SceneManager.LoadScene("Level_Hub-Tutorial");
    }
}