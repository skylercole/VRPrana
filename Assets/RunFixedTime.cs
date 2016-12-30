using UnityEngine;

public class RunFixedTime : MonoBehaviour
{
    public float TimeToRunSeconds;
    float starttime;

    // Use this for initialization
    void Start ()
	{
        starttime = Time.time;
        TimeToRunSeconds = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].WholeTime;
    }
	
	// Update is called once per frame
	void Update () {
        var timecount = Time.time - starttime;
        if (timecount >= TimeToRunSeconds)
            Application.Quit();
    }
}