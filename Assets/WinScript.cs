using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScript : MonoBehaviour
{
    public PlayerScript playerOne; // an array of all the players in the scene
    public PlayerScript playerTwo; // an array of all the players in the scene
    public GameObject[] winUi; // an array of all the players in the scene
    public GameObject[] loseUi; // an array of all the players in the scene

    // Start is called before the first frame update
    void Start()
    {
        playerOne = GameObject.Find("Professor Proton").GetComponent<PlayerScript>();
        playerTwo = GameObject.Find("Dr Diode").GetComponent<PlayerScript>();

        winUi = GameObject.FindGameObjectsWithTag("Win UI");
        loseUi = GameObject.FindGameObjectsWithTag("Lose UI");

        if (playerOne.GetScore() > playerTwo.GetScore())
        {
            winUi[0].SendMessage("UpdateText", playerOne.name);
            loseUi[0].SendMessage("UpdateText", playerTwo.name);

        }
        else
        {
            winUi[0].SendMessage("UpdateText", playerTwo.name);
            loseUi[0].SendMessage("UpdateText", playerOne.name);
        }


    }
}
