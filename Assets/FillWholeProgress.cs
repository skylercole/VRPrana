using UnityEngine;
using UnityEngine.UI;

public class FillWholeProgress : MonoBehaviour
{

    public Image circularSlider;            //Drag the circular image i.e Slider in our case
    public int time;                      //In how much time the progress bar will fill/empty

    void Start()
    {
        circularSlider.fillAmount = 0;      // Initally progress bar is empty
        time = ProgressTypes.ProgressTypeList[GlobalVariables.MeditationPractice].WholeTime;
    }
    void Update()
    {
        if (circularSlider.fillAmount >= 1f)
            circularSlider.fillAmount = 0f;

        circularSlider.fillAmount += Time.deltaTime / time;
    }
}