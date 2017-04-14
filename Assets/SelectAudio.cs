using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectAudio: MonoBehaviour, IPointerClickHandler
{
    public Button Button;
    private bool isButtonPressed;
    public AudioSource audioSource;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isButtonPressed) // previous status
        {
            Button.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("loudspeaker_pause");
            audioSource.Stop();
            GlobalVariables.SoundOn = 0;
        }
        else
        {
            Button.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("loudspeaker_run");
            audioSource.Play();
            GlobalVariables.SoundOn = 1;
        }

        isButtonPressed = !isButtonPressed;
    }
}