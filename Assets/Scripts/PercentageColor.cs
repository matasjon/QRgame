using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class PercentageColor : MonoBehaviour
{
    public int percentage;
    public TextMeshProUGUI text_field;
    

    void Start()
    {
        percentage = 0;
    }

    void Update()
    {
        text_field.text = percentage.ToString() + "%";

        if (percentage < 33)
        {
            text_field.color = Color.red;
        }
        else if (percentage >= 33 && percentage < 66)
        {
            text_field.color = Color.yellow;
        }
        else if (percentage >= 66 && percentage <= 100)
        {
            text_field.color = Color.green;
        }
    }
}
