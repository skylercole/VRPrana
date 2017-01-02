﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace CurvedUI {

    /// <summary>
    /// This script show you how to access the state of any button on Vive Controller via CurvedUI scripts. We use right controller as an example
    /// </summary>
    public class CUI_ViveButtonState : MonoBehaviour
    {

        enum ViveButton
        {
            Trigger,
            TouchpadTouch,
            TouchpadPress,
            Grip,
            Menu,

        }

#pragma warning disable 414 // this is just so we wont get "unused variable" code warnings when compiling without Vive.
        [SerializeField]
        Color ActiveColor = Color.green;
        [SerializeField]
        Color InActiveColor = Color.gray;
        [SerializeField] ViveButton ShowStateFor = ViveButton.Trigger;
#pragma warning restore 414


#if CURVEDUI_VIVE
        // Update is called once per frame
        void Update()
        {
           
            if(CurvedUIInputModule.Right == null)
            {
                Debug.LogError("Right controller not found - it may be off");
                return;
            }

            bool pressed = false;

            switch (ShowStateFor)
            {
                case ViveButton.Trigger:
                {
                    pressed = CurvedUIInputModule.Right.IsTriggerPressed;
                    break;
                }
                case ViveButton.TouchpadPress:
                {
                    pressed = CurvedUIInputModule.Right.IsTouchpadPressed;
                    break;
                }
                case ViveButton.TouchpadTouch:
                {
                    pressed = CurvedUIInputModule.Right.IsTouchpadTouched;
                    break;
                }
                case ViveButton.Grip:
                {
                    pressed = CurvedUIInputModule.Right.IsGripPressed;
                    break;
                }
                case ViveButton.Menu:
                {
                    pressed = CurvedUIInputModule.Right.IsApplicationMenuPressed;
                    break;
                }
            }

            this.GetComponentInChildren<Image>().color = pressed ? ActiveColor : InActiveColor;
        }
#endif
    }
}


