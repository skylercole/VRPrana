using UnityEngine;
using UnityEngine.UI;

public class ChangeProgressTexture : MonoBehaviour
{
    // Use this for initialization
    void Start () {
        GetComponent<Image>().sprite = Resources.Load("progress" + (int)(GlobalVariables.MeditationPractice + 1), typeof(Sprite)) as Sprite;
	}
}