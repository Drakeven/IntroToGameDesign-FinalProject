using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script to set the colour of the GameObject it is attached to
 */
public class SetColor : MonoBehaviour
{
    public Color ObjectColor = new Color(255, 255, 255, 255); // a white colour

    void Start()
    {
        // set the GameObject's material colour to that of the ObjectColor variable
        //gameObject.GetComponent<Renderer>().material.color = ObjectColor;
    }

    void SetNewColor(Color newColor)
    {
        // set the GameObject's material colour to that of a new colour
        gameObject.GetComponent<Renderer>().material.color = newColor;
    }
}