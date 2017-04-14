using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectBreathControl: MonoBehaviour, IPointerClickHandler
{
    public Button Button;
    public int ButtonNumber;
    //public Text BreathInfo;
    //public Image InfoImage;
    public static bool[] isButtonPressed = new bool[7];
    public Image RightMenu;

    private static Color32 pressedColor = new Color32(0x0B, 0x00, 0x8B, 0x75);
    private static Color32 normalColor = new Color32(0xFF, 0xFF, 0xFF, 0x00);

    void Start()
    {
        if (isButtonPressed[ButtonNumber])
        {
            Button.GetComponent<Image>().color = pressedColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isButtonPressed[ButtonNumber]) // previous status
        {
            Array.Clear(isButtonPressed, 0, isButtonPressed.Length);

            //BreathInfo.text = ProgressTypes.ProgressTypeList[ButtonNumber].Description;
            //InfoImage.overrideSprite = Resources.Load<Sprite>("menucircle" + (ButtonNumber+1));
            //InfoImage.color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);

            // Change right canvas's sprite.
            RightMenu.enabled = true;
            RightMenu.overrideSprite = Resources.Load<Sprite>("rightmenu_" + (ButtonNumber + 1));
        }
        else
        {
            //BreathInfo.text = "";
            //InfoImage.overrideSprite = new Sprite();
            //InfoImage.color = new Color32(0xFF, 0xFF, 0xFF, 0x00);

            RightMenu.enabled = false;
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