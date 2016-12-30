using UnityEngine;
using System.Collections;

public class CiclularProgress : MonoBehaviour {
	
	public int timeToComplete = 3;
	
	// Use this for initialization
	void Start () {
		//Use this to Start progress
		StartCoroutine(RadialProgress(timeToComplete));
	}
	
	IEnumerator RadialProgress(float time)
	{
		float rate = 1 / time;
		float i = 0;
	    while (true)
	    {
            while (i < 1)
	        {
	            i += Time.deltaTime*rate;
	            gameObject.GetComponent<Renderer>().material.SetFloat("_Progress", i);
	            yield return 0;
	        }

            i = Time.deltaTime * rate * 5; // reset and catch up with lost time
        }
	}
}