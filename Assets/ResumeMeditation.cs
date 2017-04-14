using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeMeditation : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        VideoSceneBackButtonMenu.IsPausing = false;
    }
}   