using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinscript : MonoBehaviour
{
    public Vector3[] coinpos;
    bool coinUp = false;
    int coinLoc = 0;
    System.Random rnd = new System.Random();
    AudioSource sound;
    LevelController levelController; // the Level Controller of the level

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        changeLoc(randomPos());

        // find the Level Controller in the scene
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            sound.Play();
            coinUp = true;
            changeLoc(randomPos());
            if (levelController != null)
            {
                levelController.InitiateSwap();
            }
        }
    }

    public void setcoinUp(bool setter)
    {
        coinUp = setter;
    }

    public bool getcoinUp()
    {
        return coinUp;
    }

    public void changeLoc(int location)
    {
        transform.position = coinpos[location];
    }

    public int randomPos()
    {
        return rnd.Next(0, coinpos.Length);
    }
}