using UnityEngine;
using System;

public class ChangeCycleTime : MonoBehaviour
{
    private float starttime;
    public static int InitalCounter = 3;

    void Awake()
    {
        InitalCounter = 3;
    }

    // Use this for initialization
    void Start ()
    {
        starttime = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].CycleTime;
        GetComponent<TextMesh>().text = String.Format("{00:00}", InitalCounter);
        InvokeRepeating("decreaseTimeRemaining", 1.0f, 1.0f);
    }
	
    void decreaseTimeRemaining()
    {
        if (InitalCounter > 1)
        {
            InitalCounter--;
            GetComponent<TextMesh>().text = String.Format("{0:00}", InitalCounter);
        }
        else if (InitalCounter == 1)
        {
            InitalCounter--;
            GetComponent<TextMesh>().text = String.Format("{0:00}", starttime);
        }
        else
        {
            starttime--;

            if (starttime == 0)
                starttime = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].CycleTime;

            GetComponent<TextMesh>().text = String.Format("{0:00}", starttime);
        }
    }
}