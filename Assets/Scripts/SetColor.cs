using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    public Color ObjectColor = new Color(200, 200, 200, 255);

    void Start()
    {
        //Color ObjectColor = new Color(Random.value, Random.value, Random.value);
        gameObject.GetComponent<Renderer>().material.color = ObjectColor;
    }

    void setNewColor(Color newColor)
    {
        gameObject.GetComponent<Renderer>().material.color = newColor;
    }
}