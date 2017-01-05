using UnityEngine;
using System;

public class ChangeCycleTime : MonoBehaviour
{
    float starttime;

    // Use this for initialization
    void Start ()
    {
        starttime = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].CycleTime;
        GetComponent<TextMesh>().text = String.Format("{00:00}", starttime);
        InvokeRepeating("decreaseTimeRemaining", 1.0f, 1.0f);
    }
	
    void decreaseTimeRemaining()
    {
        starttime--;

        if (starttime == 0)
            starttime = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].CycleTime;

        GetComponent<TextMesh>().text = String.Format("{00:00}", starttime);
    }
}