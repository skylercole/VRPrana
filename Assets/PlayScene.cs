using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var found = Array.FindIndex(SelectBreathControl.isButtonPressed, x => x);

        if (found == -1)
        {
            GlobalVariables.MeditationPractice = 0;
        }
        else
        {
            GlobalVariables.MeditationPractice = found;
        }
        
        SettingsManager.PersistData();
        GlobalVariables.MeditationPractice = Array.FindIndex(SelectBreathControl.isButtonPressed, x => x == true);

        SceneManager.LoadSceneAsync("Scenes/Demo_Sphere");
    }
}