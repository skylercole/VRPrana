using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectScene: MonoBehaviour, IPointerClickHandler
{
    public Button Button;
    public int ButtonNumber;
    private static bool[] isButtonPressed = new bool[1];
    private static Color32 pressedColor = new Color32(0x0B, 0x00,0x8B,0xB9);
    private static Color32 normalColor = new Color32(0xFF, 0xFF, 0xFF, 0x00);

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isButtonPressed[ButtonNumber]) // previous status
        {
            Array.Clear(isButtonPressed, 0, isButtonPressed.Length);
        }

        isButtonPressed[ButtonNumber] = !isButtonPressed[ButtonNumber];
    }

    void Update()
    {
        if (!isButtonPressed[ButtonNumber] && Button.GetComponent<Image>().color == pressedColor)
        {
            Button.GetComponent<Image>().color = normalColor;
        }
        else if (isButtonPressed[ButtonNumber] && Button.GetComponent<Image>().color == normalColor)
        {
            Button.GetComponent<Image>().color = pressedColor;
        }
    }
}