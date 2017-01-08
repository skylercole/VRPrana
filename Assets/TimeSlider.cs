using System;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class TimeSlider : MonoBehaviour
{
    public Slider mainSlider;
    public Text timeText;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        timeText.text = String.Format("{00:00}:00", mainSlider.value);
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        timeText.text = String.Format("{00:00}:00", mainSlider.value);
        GlobalVariables.TimeMinutes = Convert.ToInt32(mainSlider.value);
    }
}