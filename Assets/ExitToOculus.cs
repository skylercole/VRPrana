using UnityEngine;
using UnityEngine.EventSystems;

public class ExitToOculus : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}