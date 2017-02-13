using System;
using CurvedUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour, IPointerClickHandler
{
    public int SceneSelected = 0;
    public CurvedUISettings Settings;

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
        
        SettingsManager.PersistData(SceneSelected);


        switch (SceneSelected)
        {
            case 0:
                GlobalVariables.SceneToPlay = "frozenforest.mp4";
                break;
            case 1:
                GlobalVariables.SceneToPlay = "frozenlake.mp4";
                break;
        }


        SceneManager.LoadSceneAsync("Scenes/Demo_Sphere");
    }
}