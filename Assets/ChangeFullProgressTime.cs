using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ChangeFullProgressTime : MonoBehaviour
{
    private float starttime;
    private TimeSpan t;

    void Awake()
    {
        starttime = GlobalVariables.TimeMinutes * 60;
        var calc = starttime % ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].CycleTime;
        if (calc > 0)
        {
            starttime -= calc;
            starttime += ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].CycleTime;
        }

        ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].WholeTime = Convert.ToInt32(starttime);

        t = TimeSpan.FromSeconds(starttime);

        GetComponent<TextMesh>().text = "";
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("decreaseTimeRemaining", 1.0f, 1.0f);
    }

    void decreaseTimeRemaining()
    {
        if (ChangeCycleTime.InitalCounter > 1)
        {
            GetComponent<TextMesh>().text = "";
        }
        else if (ChangeCycleTime.InitalCounter == 1)
        {
            GetComponent<TextMesh>().text = String.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
        }
        else if (starttime > 1)
        {
            starttime--;
            t = TimeSpan.FromSeconds(starttime);
            GetComponent<TextMesh>().text = String.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
        }
        else if (starttime <= 2)
        {
            SceneManager.LoadScene("Scenes/MenuScene");
        }
    }
}