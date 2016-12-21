using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeTime : MonoBehaviour
{
    int min;
    int sec;
    float timecount;
    float starttime;

    // Use this for initialization
    void Start () {
        starttime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timecount = Time.time - starttime;
        min = Convert.ToInt32(timecount / 60f);
        sec = Convert.ToInt32(timecount % 60f);
        GetComponent<TextMesh>().text = String.Format("{00:00}:{1:00}", min, sec);
    }
}