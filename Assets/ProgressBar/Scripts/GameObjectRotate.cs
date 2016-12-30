using UnityEngine;
using System.Collections;

public class GameObjectRotate : MonoBehaviour
{
    private const int frames = 5;
    private int f = 0;
    private const int count = 20;
    private int i = 0;
    Vector3 vec = Vector3.up;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    f++;

	    if (f == frames)
	    {
            i++;
            if (i == count)
            {
                i -= count * 2;
                vec = vec == Vector3.up ? Vector3.down : Vector3.up;
            }
            transform.Rotate(vec, Time.deltaTime * 100.0f);
	        f = 0;
	    }
	    
	}
}
