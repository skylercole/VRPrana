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
		bool loadingCustomDefine = false;
#pragma warning restore 414



		#region LIFECYCLE
        void Start()
		{
			AddCurvedUIComponents();
		}
			
		void OnEnable()
		{
			EditorApplication.hierarchyWindowChanged += AddCurvedUIComponents;
			loadingCustomDefine = false;

		}

		void OnDisable()
		{
			EditorApplication.hierarchyWindowChanged -= AddCurvedUIComponents;
		}
		#endregion 




        public override void OnInspectorGUI() {
        CurvedUISettings myTarget = (CurvedUISettings)target;

        if (target == null) return;

        //initial settings
        GUI.changed = false;
        EditorGUIUtility.labelWidth = 150;

		//global setting - control methods
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
			//show advanced settings enable button
            if (GUILayout.Button("Show Advanced Settings")) {
                ShowAdvaced = true;
				loadingCustomDefine = false;
             }

        } else {
			// or just show advanced settings

			//hide advances settings button.
            if (GUILayout.Button("Hide Advanced Settings")) ShowAdvaced = false;
                GUILayout.Space(20);


			//common options
            GUILayout.Label("Other Options", EditorStyles.boldLabel);
            myTarget.Interactable = EditorGUILayout.Toggle("Interactable", myTarget.Interactable);
			myTarget.BlocksRaycasts = EditorGUILayout.Toggle("Blocks Raycasts", myTarget.BlocksRaycasts);
            myTarget.RaycastMyLayerOnly = EditorGUILayout.Toggle("Raycast My Layer Only", myTarget.RaycastMyLayerOnly);
            myTarget.Quality = EditorGUILayout.Slider("Quality", myTarget.Quality, 0.1f, 3.0f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(150);
            GUILayout.Label("Smoothness of the curve. Bigger values mean more subdivisions. Decrease for better performance. Default 1", EditorStyles.helpBox);
            GUILayout.EndHorizontal();


			//add components button
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Components", GUILayout.Width(146));
			if (GUILayout.Button("Add Effect To Children"))AddCurvedUIComponents();
            GUILayout.EndHorizontal();


			//remove components button
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
			GUILayout.BeginVertical();

            switch (CurvedUIInputModule.Controller)
            {
                case CurvedUIInputModule.CurvedUIController.MOUSE:
                {

#if CURVEDUI_GOOGLEVR
					EditorGUILayout.HelpBox("Enabling this control method will disable GoogleVR Gaze support.", MessageType.Warning);

					DrawCustomDefineSwitcher("");
#else
                    GUILayout.Label("Basic Controller. Mouse on screen", EditorStyles.helpBox);
					#endif
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.GAZE:
                {
#if CURVEDUI_GOOGLEVR
					EditorGUILayout.HelpBox("Enabling this control method will disable GoogleVR Gaze support.", MessageType.Warning);

					DrawCustomDefineSwitcher("");

#else
                    GUILayout.Label("Center of Canvas's World Camera acts as a pointer. This is a generic gaze implementation, to be used with any headset. If you're on cardboard, use GOOGLEVR control method for Reticle and GameObject interaction support.", EditorStyles.helpBox);
					#endif
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.WORLD_MOUSE:
                {

#if CURVEDUI_GOOGLEVR
					EditorGUILayout.HelpBox("Enabling this control method will disable GoogleVR Gaze support.", MessageType.Warning);

					DrawCustomDefineSwitcher("");
#else
                    GUILayout.Label("Mouse controller that is independent of the camera view. Use WorldSpaceMouseOnCanvas function to get its position.", EditorStyles.helpBox);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(150);
					CurvedUIInputModule.Instance.WorldSpaceMouseSensitivity = EditorGUILayout.FloatField("Mouse Sensitivity", CurvedUIInputModule.Instance.WorldSpaceMouseSensitivity);
					#endif
					break;
                }
                case CurvedUIInputModule.CurvedUIController.CUSTOM_RAY:
                {
#if CURVEDUI_GOOGLEVR
					EditorGUILayout.HelpBox("Enabling this control method will disable GoogleVR Gaze support.", MessageType.Warning);

					DrawCustomDefineSwitcher("");
#else
                    GUILayout.Label("Set a ray used to interact with canvas using CustomControllerRay function. Use CustromControllerButtonDown bool to set button pressed state. \nYou can find both in CurvedUIInputModule class", EditorStyles.helpBox);
					#endif
					break;
                }
                case CurvedUIInputModule.CurvedUIController.DAYDREAM:
                {
#if CURVEDUI_GOOGLEVR
					EditorGUILayout.HelpBox("Enabling this control method will disable GoogleVR Gaze Reticle support. It's counter-intuitive, but if you're working with daydream controller - you want this.", MessageType.Warning);

					DrawCustomDefineSwitcher("");
#else
                    GUILayout.Label("Set a ray used to find selected objects with CustomControllerRay function. Use CustromControllerButtonDown bool to set button pressed state. /n You can find both of these in CurvedUIInputModule", EditorStyles.helpBox);
					#endif
					break;
                }
                case CurvedUIInputModule.CurvedUIController.VIVE:
                {

#if CURVEDUI_VIVE
                    // vive enabled, we can show settings
                    GUILayout.Label("Use one or both vive controllers as to interact with canvas. Trigger acts a button", EditorStyles.helpBox);
                    CurvedUIInputModule.Instance.UsedVRController = (CurvedUIInputModule.ActiveVRController)EditorGUILayout.EnumPopup("Used Controller", CurvedUIInputModule.Instance.UsedVRController);

#else
					DrawCustomDefineSwitcher("CURVEDUI_VIVE");

#endif
                    break;
                }
                case CurvedUIInputModule.CurvedUIController.OCULUS_TOUCH:
                {

#if CURVEDUI_TOUCH
                    // oculus enabled, we can show settings
                    GUILayout.Label("Use Touch controller to interact with canvas.", EditorStyles.helpBox);

                    //transform property
                    SerializedObject serializedInputModule = new UnityEditor.SerializedObject(CurvedUIInputModule.Instance);
                    serializedInputModule.Update();
                    EditorGUILayout.PropertyField(serializedInputModule.FindProperty("TouchControllerTransform"));
                    serializedInputModule.ApplyModifiedProperties();

                    //button property
                    CurvedUIInputModule.Instance.OculusTouchInteractionButton = (OVRInput.Button)EditorGUILayout.EnumPopup("Interaction Button", CurvedUIInputModule.Instance.OculusTouchInteractionButton);
#else
					DrawCustomDefineSwitcher("CURVEDUI_TOUCH");
#endif
                    break;
                }
				case CurvedUIInputModule.CurvedUIController.GOOGLEVR:
				{
					
#if CURVEDUI_GOOGLEVR
					GUILayout.Label("Use GoogleVR Reticle to interact with canvas.", EditorStyles.helpBox);
#else
					DrawCustomDefineSwitcher("CURVEDUI_GOOGLEVR");
#endif
					break;
				}
            }

			GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }



		/// <summary>
		/// Draws the define switcher for different control methods. 
		/// Because different control methods use different API's that may not always be available, 
		/// CurvedUI needs to be recompile with different custom defines to fix this. This method 
		/// manages the defines.
		/// </summary>
		/// <param name="switcho">Switcho.</param>
		void DrawCustomDefineSwitcher(string switcho)
		{
			GUILayout.BeginVertical();
			GUILayout.Label("Press the button below to make CurvedUI recompile for this control method. It should take less than 30 seconds. Afterwards, you'll see its settings here.", EditorStyles.helpBox);

			GUILayout.BeginHorizontal();
			GUILayout.Space(50);
			if (GUILayout.Button(loadingCustomDefine ? "Please wait..." : "Enable."))
			{
				loadingCustomDefine = true;
				string str = "";
				str += PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

				//remove curvedui defines
				foreach(string define in new string[]{"CURVEDUI_VIVE","CURVEDUI_TOUCH","CURVEDUI_GOOGLEVR"}){
					if (str.Contains(define))
					{
						if(str.Contains((";" + define)))
							str = str.Replace((";" + define), "");
						else
 							str = str.Replace(define, "");
					}
				}


				//add this one, if not present.
				if (switcho != "" && !str.Contains(switcho))
				{
					str += ";" + switcho;
				}

				Debug.Log ("submitted str: " + str);

				PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, str);

			}
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
		}



		/// <summary>
		///Travel the hierarchy and add CurvedUIVertexEffect to every gameobject that can be bent.
		/// </summary>
        private void AddCurvedUIComponents()
        {
            if (target == null) return;

            (target as CurvedUISettings).AddEffectToChildren();

        }



		/// <summary>
		/// Removes all CurvedUI components from this canvas.
		/// </summary>
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

