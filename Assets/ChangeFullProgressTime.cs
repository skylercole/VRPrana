using UnityEngine;
using System;

public class ChangeFullProgressTime : MonoBehaviour
{
    float starttime;
    private float min;
    private float sec;

    // Use this for initialization
    void Start()
    {
        starttime = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].WholeTime;
        CalcAndDraw();
        InvokeRepeating("decreaseTimeRemaining", 1.0f, 1.0f);
    }

    void decreaseTimeRemaining()
    {
        starttime--;
        CalcAndDraw();
    }

    void CalcAndDraw()
    {
        min = starttime/60;
        sec = starttime%60;
        GetComponent<TextMesh>().text = String.Format("{00:00}:{1:00}", min, sec);
    }
}