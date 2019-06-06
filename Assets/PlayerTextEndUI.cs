using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTextEndUI : MonoBehaviour
{
    private TextMeshProUGUI thisText;

    void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    public void UpdateText(string text)
    {
        thisText.text = text;
    }
}
