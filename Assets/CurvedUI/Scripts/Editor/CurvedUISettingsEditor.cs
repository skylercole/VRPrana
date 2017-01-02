using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;

#if CURVEDUI_TMP
using TMPro;
#endif 


namespace CurvedUI { 
 
[ExecuteInEditMode]
[CustomEditor(typeof(CurvedUISettings))]
public class CurvedUISettingsEditor : Editor {

#pragma warning disable 414
        bool ShowRemoveCurvedUI = false;
        static bool ShowAdvaced = false;

        bool enablingVive = false;
        bool enablingTouch = false;
#pragma warning restore 414


        void Start(){
		AddCurvedUIComponents();
	}

        public override void OnInspectorGUI() {
        CurvedUISettings myTarget = (CurvedUISettings)target;

        if (target == null) return;

        //initial settings
        GUI.changed = false;
        EditorGUIUtility.labelWidth = 150;

            DrawControlMethods();

        //shape settings
        GUILayout.Label("Shape", EditorStyles.boldLabel);
        myTarget.Shape = (CurvedUISettings.CurvedUIShape)EditorGUILayout.EnumPopup("Canvas Shape", myTarget.Shape);
        switch (myTarget.Shape) {
            case CurvedUISettings.CurvedUIShape.CYLINDER: {
                myTarget.Angle = EditorGUILayout.IntSlider("Angle", myTarget.Angle, -360, 360);
                myTarget.PreserveAspect = EditorGUILayout.Toggle("Preserve Aspect", myTarget.PreserveAspect);

                break;
            }
            case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
            {
                myTarget.Angle = EditorGUILayout.IntSlider("Angle", myTarget.Angle, -360, 360);
                myTarget.PreserveAspect = EditorGUILayout.Toggle("Preserve Aspect", myTarget.PreserveAspect);

                break;
            }
            case CurvedUISettings.CurvedUIShape.RING: {
                myTarget.RingExternalDiameter = Mathf.Clamp(EditorGUILayout.IntField("External Diameter", myTarget.RingExternalDiameter), 1, 100000);
                myTarget.Angle = EditorGUILayout.IntSlider("Angle", myTarget.Angle, 0, 360);
                myTarget.RingFill = EditorGUILayout.Slider("Fill", myTarget.RingFill, 0.0f, 1.0f);
                myTarget.RingFlipVertical = EditorGUILayout.Toggle("Flip Canvas Vertically", myTarget.RingFlipVertical);
                break;
            }
            case CurvedUISettings.CurvedUIShape.SPHERE: {
                GUILayout.BeginHorizontal();
                GUILayout.Space(150);
                GUILayout.Label("Sphere shape is more expensive than a Cyllinder shape. Keep this in mind when working on mobile VR.", EditorStyles.helpBox);
                GUILayout.EndHorizontal();
                GUILayout.Space(10);

                if (myTarget.PreserveAspect) {
                    myTarget.Angle = EditorGUILayout.IntSlider("Angle", myTarget.Angle, -360, 360);
                } else {
                    myTarget.Angle = EditorGUILayout.IntSlider("Horizontal Angle", myTarget.Angle, 0, 360);
                    myTarget.VerticalAngle = EditorGUILayout.IntSlider("Vertical Angle", myTarget.VerticalAngle, 0, 180);
                }
                myTarget.PreserveAspect = EditorGUILayout.Toggle("Preserve Aspect", myTarget.PreserveAspect);

                break;
            }
        }


           

        //advanced settings
        GUILayout.Space(10);

        if (!ShowAdvaced) {
                if (GUILayout.Button("Show Advanced Settings")) {
                    ShowAdvaced = true;
					if(enablingVive)enablingVive = false;
                 }

        } else {
            if (GUILayout.Button("Hide Advanced Settings")) ShowAdvaced = false;
                GUILayout.Space(20);



            GUILayout.Label("Other Options", EditorStyles.boldLabel);

            myTarget.Interactable = EditorGUILayout.Toggle("Interactable", myTarget.Interactable);
			myTarget.BlocksRaycasts = EditorGUILayout.Toggle("Blocks Raycasts", myTarget.BlocksRaycasts);
            myTarget.RaycastMyLayerOnly = EditorGUILayout.Toggle("Raycast My Layer Only", myTarget.RaycastMyLayerOnly);
            myTarget.Quality = EditorGUILayout.Slider("Quality", myTarget.Quality, 0.1f, 3.0f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(150);
            GUILayout.Label("Smoothness of the curve. Bigger values mean more subdivisions. Decrease for better performance. Default 1", EditorStyles.helpBox);
            GUILayout.EndHorizontal();



            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Components", GUILayout.Width(146));
            if (GUILayout.Button("Add Effect To Children"))
            {
                AddCurvedUIComponents();
            }
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(146));

            if (!ShowRemoveCurvedUI) {
                if (GUILayout.Button("Remove CurvedUI from Canvas")) ShowRemoveCurvedUI = true;
            } else {
                if (GUILayout.Button("Remove CurvedUI"))  {
                    RemoveCurvedUIComponents();
                }
                if (GUILayout.Button("Cancel"))  {
                    ShowRemoveCurvedUI = false;
                }
            }
                GUILayout.EndHorizontal();



                // Left for later, may come usefull
                //				if (GUILayout.Button("Get Defines"))  {
                //					Debug.Log(PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup));
                //						
                //				}
                //
                //				if (GUILayout.Button("Enable VIVE support"))  {
                //
                //					string str = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                //					if(!str.Contains("CURVEDUI_VIVE")){
                //						str += ";CURVEDUI_VIVE";
                //						PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, str);
                //					}
                //
                //				}
                //
                //
                //				if (GUILayout.Button("Enable TMP support"))  {
                //
                //					string str = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                //					if(!str.Contains("CURVEDUI_TMP")){
                //						str += ";CURVEDUI_TMP";
                //						PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, str);
                //					}
                //
                //				}
                //
                //
//#if CURVEDUI_TMP || CURVEDUI_VIVE || CURVEDUI_TOUCH
//                GUILayout.BeginHorizontal();
//                GUILayout.Label("", GUILayout.Width(146));
//                if (GUILayout.Button("Remove CurvedUI Custom Defines"))
//                {
//                    string str = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
//                    if (!str.Contains("CURVEDUI_TMP"))
//                    {
//                        str.Replace("CURVEDUI_TMP", "");
//                    }

//                    if (!str.Contains("CURVEDUI_TOUCH"))
//                    {
//                        str.Replace("CURVEDUI_TOUCH", "");
//                    }

//                    if (!str.Contains("CURVEDUI_VIVE"))
//                    {
//                        str.Replace("CURVEDUI_VIVE", "");

//                    }

//                    PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, str);
//                }
//                GUILayout.EndHorizontal();
//#endif


            }  // end of advanced settings

            //final settings
            if (GUI.changed)
		EditorUtility.SetDirty(myTarget);

	}



    void DrawControlMethods()
        {
            GUILayout.Label("Global Settings", EditorStyles.boldLabel);

            //controller
            CurvedUIInputModule.Controller = (CurvedUIInputModule.CurvedUIController)EditorGUILayout.EnumPopup("Control Method", CurvedUIInputModule.Controller);
            GUILayout.BeginHorizontal();
            GUILayout.Space(150);
            switch (CurvedUIInputModule.Controller)
            {
                case CurvedUIInputModule.CurvedUIController.MOUSE:
                {

                    GUILayout.Label("Basic Controller. Mouse in screen space.", EditorStyles.helpBox);
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.GAZE:
                {
                    GUILayout.Label("Center of Canvas's World Camera acts as a pointer.", EditorStyles.helpBox);
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.WORLD_MOUSE:
                {
                    GUILayout.Label("Mouse controller that is independent of the camera view. Use WorldSpaceMouseOnCanvas function to get its position.", EditorStyles.helpBox);
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(150);
                    CurvedUIInputModule.Instance.WorldSpaceMouseSensitivity = EditorGUILayout.FloatField("Mouse Sensitivity", CurvedUIInputModule.Instance.WorldSpaceMouseSensitivity);
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.CUSTOM_RAY:
                {
                    GUILayout.Label("Set a ray used to find selected objects with CustomControllerRay function. Use CustromControllerButtonDown bool to set button pressed state. /nYou can find both of these in CurvedUIInputModule", EditorStyles.helpBox);
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.DAYDREAM:
                {
                    GUILayout.Label("Set a ray used to find selected objects with CustomControllerRay function. Use CustromControllerButtonDown bool to set button pressed state. /nYou can find both of these in CurvedUIInputModule", EditorStyles.helpBox);
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.VIVE:
                {

#if CURVEDUI_VIVE
                    // vive enabled, we can show settings
                    GUILayout.Label("Use one or both vive controllers as to interact with canvas. Trigger acts a button", EditorStyles.helpBox);
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(150);
                    CurvedUIInputModule.Instance.UsedVRController = (CurvedUIInputModule.ActiveVRController)EditorGUILayout.EnumPopup("Used Controller", CurvedUIInputModule.Instance.UsedVRController);
#else
                    // vive not enabled, lets leave some info how to do it.
                    GUILayout.BeginVertical();
                    GUILayout.Label("To enable Vive support, use the button below to add \"CURVEDUI_VIVE\" to your Scripting Define Symbols. CurvedUI will recompile and you'll see Vive related settings here.", EditorStyles.helpBox);

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(50);
                    if (GUILayout.Button(enablingVive ? "Please wait..." : "Enable VIVE support"))
                    {
                        enablingVive = true;
                        string str = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                        if (!str.Contains("CURVEDUI_VIVE"))
                        {
                            str += ";CURVEDUI_VIVE";
                            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, str);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
#endif
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.OCULUS_TOUCH:
                {

#if CURVEDUI_TOUCH
                    // vive enabled, we can show settings
                    GUILayout.Label("Use Touch controller to interact with canvas.", EditorStyles.helpBox);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(150);
                    GUILayout.BeginVertical();

                    //transform property
                    SerializedObject serializedInputModule = new UnityEditor.SerializedObject(CurvedUIInputModule.Instance);
                    serializedInputModule.Update();
                    EditorGUILayout.PropertyField(serializedInputModule.FindProperty("TouchControllerTransform"));
                    serializedInputModule.ApplyModifiedProperties();

                    //button property
                    CurvedUIInputModule.Instance.OculusTouchInteractionButton = (OVRInput.Button)EditorGUILayout.EnumPopup("Interaction Button", CurvedUIInputModule.Instance.OculusTouchInteractionButton);
                    GUILayout.EndVertical();
#else
                        // vive not enabled, lets leave some info how to do it.
                        GUILayout.BeginVertical();
                        GUILayout.Label("To enable Oculus Touch, use the button below to add \"CURVEDUI_TOUCH\" to your Scripting Define Symbols. CurvedUI will recompile and you'll see Touch related settings here.", EditorStyles.helpBox);

                        GUILayout.BeginHorizontal();
                        GUILayout.Space(50);
                        if (GUILayout.Button(enablingTouch ? "Please wait..." : "Enable OCULUS TOUCH"))
                        {
                            enablingTouch = true;
                            string str = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                            if (!str.Contains("CURVEDUI_TOUCH"))
                            {
                                str += ";CURVEDUI_TOUCH";
                                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, str);
                            }
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
#endif
                    break;
                }
            }


            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }





        void OnEnable()
        {
            EditorApplication.hierarchyWindowChanged += AddCurvedUIComponents;
        }

        void OnDisable()
        {
            EditorApplication.hierarchyWindowChanged -= AddCurvedUIComponents;
        }

        //Travel the hierarchy and add CurvedUIVertexEffect to every gameobject that can be bent.
        private void AddCurvedUIComponents()
        {
            if (target == null) return;

            (target as CurvedUISettings).AddEffectToChildren();

        }

        private void RemoveCurvedUIComponents()
    {
        if (target == null) return;

        //destroy componenets
        List<CurvedUIVertexEffect> comps = new List<CurvedUIVertexEffect>();
        comps.AddRange((target as CurvedUISettings).GetComponentsInChildren<CurvedUIVertexEffect>(true));
        for (int i = 0; i < comps.Count; i++)
        {
            if (comps[i].GetComponent<UnityEngine.UI.Graphic>() != null) comps[i].GetComponent<UnityEngine.UI.Graphic>().SetAllDirty();
            DestroyImmediate(comps[i]);
            
        }

        //destroy raycasters
        List<CurvedUIRaycaster> raycasters = new List<CurvedUIRaycaster>();
        raycasters.AddRange((target as CurvedUISettings).GetComponents<CurvedUIRaycaster>());
        for (int i = 0; i < raycasters.Count; i++)
        {
            DestroyImmediate(raycasters[i]);
        }

        DestroyImmediate(target);
    }

     
    }
}

