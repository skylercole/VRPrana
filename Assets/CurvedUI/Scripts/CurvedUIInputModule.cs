using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using CurvedUI;

#if CURVEDUI_VIVE
using Valve.VR;
#endif 

[ExecuteInEditMode]
public class CurvedUIInputModule : StandaloneInputModule {



    //main references
    static CurvedUIInputModule instance;
    public static CurvedUIInputModule Instance {
        get  {
            if(instance == null)
                instance = EnableInputModule<CurvedUIInputModule>();

            return instance;
        }
        private set  { instance = value;  }
    }


    #region SETTINGS
    //SETTINGS  - COMMON
    [SerializeField]
    CurvedUIController controller;
    public string submitButtonName = "Fire1"; // name of button to use for click/submit
    //public string controlAxisName = "Horizontal";// name of axis to use for scrolling/sliders



    //SETTINGS - World Space Mouse
    float worldSpaceMouseSensitivity = 1;

    //SETTINGS - Vive
    [SerializeField]
    public ActiveVRController eventController = ActiveVRController.Right;
    #endregion




    #region VARIABLES
#pragma warning disable 414, 0649 // this is just so we wont get "unused variable" code warnings when compiling with different platform defines

    //Support Variables - common
    private GameObject currentDragging;
    private GameObject currentPointedAt;

    //Support variables - custom ray
    Ray customControllerRay;
    [SerializeField]
    Transform customControllerTransform;

    //support variables - other
    float buttonClickTime = 0.5f;
	float dragThreshold = 10.0f;
	float buttonDownTime;
	bool pressedDownExecuted = false;
	bool pressedDown = false;
	bool pressedLastFrame = false;
	GameObject buttonPressed;


    //support variables - world space mouse
    Vector3 lastMouseOnScreenPos = Vector2.zero;
    Vector2 worldSpaceMouseInCanvasSpace = Vector2.zero;
    Vector2 lastWorldSpaceMouseOnCanvas = Vector2.zero;
    Vector2 worldSpaceMouseOnCanvasDelta = Vector2.zero;


    //platform dependant references & Settings
#if CURVEDUI_VIVE
    //Settings & References - Vive
    [SerializeField]
    SteamVR_ControllerManager steamVRControllerManager;

    //Support Variables - Vive
    private static SteamVR_ControllerManager controllerManager;
    private static CurvedUIViveController rightCont;
    private static CurvedUIViveController leftCont;
    private CurvedUIPointerEventData rightControllerData;
    private CurvedUIPointerEventData leftControllerData;

#endif


#if CURVEDUI_TOUCH
    [SerializeField]
    Transform TouchControllerTransform;

    [SerializeField]
    OVRInput.Button InteractionButton = OVRInput.Button.PrimaryIndexTrigger;
#endif
#pragma warning restore 414, 0649

    //enums
    public enum CurvedUIController
    {
        MOUSE = 0,
        GAZE = 1,
        WORLD_MOUSE = 2,
        CUSTOM_RAY = 3,
        VIVE = 4,
        OCULUS_TOUCH = 5,
        DAYDREAM = 6,
    }

    public enum ActiveVRController
    {
        Both = 0,
        Right = 1,
        Left = 2,
    }
    #endregion 

    #region LIFECYCLE
    protected override void Awake(){

        if (!Application.isPlaying) return;

		Instance = this;
		base.Awake ();


#if CURVEDUI_VIVE
        //SEtup controllers for vive
        if(Controller == CurvedUIController.VIVE)
             SetupViveControllers();
#endif
    }


    void Update()
    {
        ////moving the world space mouse
        //if (Controller == CurvedUIController.WORLD_MOUSE)
        //{
        //    //touch can also be used to control a world space mouse, although its probably not the best experience
        //    //Use standard mouse controller with touch.
        //    if (Input.touchCount > 0)
        //    {
        //        worldSpaceMouseOnCanvasDelta = Input.GetTouch(0).deltaPosition * worldSpaceMouseSensitivity;
        //    }
        //    else {
        //        worldSpaceMouseOnCanvasDelta = new Vector2((Input.mousePosition - lastMouseOnScreenPos).x, (Input.mousePosition - lastMouseOnScreenPos).y) * worldSpaceMouseSensitivity;
        //        lastMouseOnScreenPos = Input.mousePosition;
        //    }
        //    lastWorldSpaceMouseOnCanvas = worldSpaceMouseInCanvasSpace;
        //    worldSpaceMouseInCanvasSpace += worldSpaceMouseOnCanvasDelta;
        //}

    }
#endregion

#region EVENT PROCESSING - GENERAL
    /// <summary>
    /// Process is called by UI system to process events 
    /// </summary>
    public override void Process()
    {

        switch (controller)
        {
            case CurvedUIController.MOUSE:
            {
                base.Process();
                break;
            }
            case CurvedUIController.GAZE:
            {
                ProcessGaze();
                break;
            }
            case CurvedUIController.VIVE:
            {
                ProcessViveControllers();
                break;
            }
            case CurvedUIController.OCULUS_TOUCH:
            {
                ProcessOculusTouchController();
                break;
            }
            case CurvedUIController.WORLD_MOUSE:
            {
                //touch can also be used as a world space mouse, although its probably not the best experience
                //Use standard mouse controller with touch.
                if (Input.touchCount > 0)
                {
                    worldSpaceMouseOnCanvasDelta = Input.GetTouch(0).deltaPosition * worldSpaceMouseSensitivity;
                } else {
                    worldSpaceMouseOnCanvasDelta = new Vector2((Input.mousePosition - lastMouseOnScreenPos).x, (Input.mousePosition - lastMouseOnScreenPos).y) * worldSpaceMouseSensitivity;
                    lastMouseOnScreenPos = Input.mousePosition;
                }
                lastWorldSpaceMouseOnCanvas = worldSpaceMouseInCanvasSpace;
                worldSpaceMouseInCanvasSpace += worldSpaceMouseOnCanvasDelta;

                base.Process();

                break;
            }
            case CurvedUIController.CUSTOM_RAY:
            {
                ProcessCustomRayController();
                break;
            }
            case CurvedUIController.DAYDREAM:
            {
                ProcessCustomRayController();
                break;
            }
            default: goto case CurvedUIController.MOUSE;
        }
    }
    #endregion



    #region EVENT PROCESSING - GAZE
    protected virtual void ProcessGaze()
    {
        bool usedEvent = SendUpdateEventToSelectedObject();

        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();

            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }

        ProcessMouseEvent();
    }

    #endregion


    #region EVENT PROCESSING - VIVE
    protected virtual void ProcessViveControllers()
    {
#if CURVEDUI_VIVE
        switch (eventController)
        {
            case ActiveVRController.Right:
            {
                //in case only one controller is turned on, it will still be used to call events.
                if (controllerManager.right.activeInHierarchy)
                    ProcessController(controllerManager.right);
                else if (controllerManager.left.activeInHierarchy)
                    ProcessController(controllerManager.left);
                break;
            }
            case ActiveVRController.Left:
            {
                //in case only one controller is turned on, it will still be used to call events.
                if (controllerManager.left.activeInHierarchy)
                    ProcessController(controllerManager.left);
                else if (controllerManager.right.activeInHierarchy)
                    ProcessController(controllerManager.right);
                break;
            }
            case ActiveVRController.Both:
            {
                ProcessController(controllerManager.left);
                ProcessController(controllerManager.right);
                break;
            }
            default: goto case ActiveVRController.Right;
        }
    }


    /// <summary>
    /// Processes Events from given controller.
    /// </summary>
    /// <param name="myController"></param>
    void ProcessController(GameObject myController)
    {
        //do not process events from this controller if it's off or not visible by base stations.
        if (!myController.gameObject.activeInHierarchy) return;

        //get the assistant or add it if its missing.
        CurvedUIViveController myControllerAssitant = myController.AddComponentIfMissing<CurvedUIViveController>();

        // send update events if there is a selected object - this is important for InputField to receive keyboard events
        SendUpdateEventToSelectedObject();

        // see if there is a UI element that is currently being pointed at
        PointerEventData ControllerData;
        if(myControllerAssitant == Right)
            ControllerData = GetControllerPointerData(myControllerAssitant, ref rightControllerData);
         else
            ControllerData = GetControllerPointerData(myControllerAssitant, ref leftControllerData);


        currentPointedAt = ControllerData.pointerCurrentRaycast.gameObject;

        ProcessDownRelease(ControllerData, myControllerAssitant.IsTriggerDown, myControllerAssitant.IsTriggerUp);

        //Process move and drag if trigger is pressed
        if (!myControllerAssitant.IsTriggerUp)
        {
            ProcessMove(ControllerData);
            ProcessDrag(ControllerData);
        }

        if (!Mathf.Approximately(ControllerData.scrollDelta.sqrMagnitude, 0.0f))
        {
            var scrollHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(ControllerData.pointerCurrentRaycast.gameObject);
            ExecuteEvents.ExecuteHierarchy(scrollHandler, ControllerData, ExecuteEvents.scrollHandler);
            // Debug.Log("executing scroll handler");
        }

    }

    /// <summary>
    /// Create a pointerEventData that stores all the data associated with Vive controller.
    /// </summary>
    private CurvedUIPointerEventData GetControllerPointerData(CurvedUIViveController controller, ref CurvedUIPointerEventData ControllerData)
    {

        if (ControllerData == null)
            ControllerData = new CurvedUIPointerEventData(eventSystem);

        ControllerData.Reset();
        ControllerData.delta = Vector2.one; // to trick into moving
        ControllerData.position = Vector2.zero; // this will be overriden by raycaster
        ControllerData.Controller = controller.gameObject; // raycaster will use this object to override pointer position on screen. Keep it safe.
        ControllerData.scrollDelta = controller.TouchPadAxis - ControllerData.TouchPadAxis; // calcualte scroll delta
        ControllerData.TouchPadAxis = controller.TouchPadAxis; // assign finger position on touchpad

        eventSystem.RaycastAll(ControllerData, m_RaycastResultCache); //Raycast all the things!. Position will be overridden here by CurvedUIRaycaster

        //Get a current raycast to find if we're pointing at GUI object. 
        ControllerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_RaycastResultCache.Clear();

        return ControllerData;
    }


    /// <summary>
    /// Sends trigger down / trigger released events to gameobjects under the pointer.
    /// </summary>
    protected virtual void ProcessDownRelease(PointerEventData ControllerData, bool down, bool released)
    {
        var currentOverGo = ControllerData.pointerCurrentRaycast.gameObject;

        // PointerDown notification
        if (down)
        {
            ControllerData.eligibleForClick = true;
            ControllerData.delta = Vector2.zero;
            ControllerData.dragging = false;
            ControllerData.useDragThreshold = true;
            ControllerData.pressPosition = ControllerData.position;
            ControllerData.pointerPressRaycast = ControllerData.pointerCurrentRaycast;

            DeselectIfSelectionChanged(currentOverGo, ControllerData);

            if (ControllerData.pointerEnter != currentOverGo)
            {
                // send a pointer enter to the touched element if it isn't the one to select...
                HandlePointerExitAndEnter(ControllerData, currentOverGo);
                ControllerData.pointerEnter = currentOverGo;
            }

            // search for the control that will receive the press
            // if we can't find a press handler set the press
            // handler to be what would receive a click.
            var newPressed = ExecuteEvents.ExecuteHierarchy(currentOverGo, ControllerData, ExecuteEvents.pointerDownHandler);

            // didnt find a press handler... search for a click handler
            if (newPressed == null)
                newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);


            float time = Time.unscaledTime;

            if (newPressed == ControllerData.lastPress)
            {
                var diffTime = time - ControllerData.clickTime;
                if (diffTime < 0.3f)
                    ++ControllerData.clickCount;
                else
                    ControllerData.clickCount = 1;

                ControllerData.clickTime = time;
            }
            else
            {
                ControllerData.clickCount = 1;
            }

            ControllerData.pointerPress = newPressed;
            ControllerData.rawPointerPress = currentOverGo;

            ControllerData.clickTime = time;

            // Save the drag handler as well
            ControllerData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);

            if (ControllerData.pointerDrag != null)
                ExecuteEvents.Execute(ControllerData.pointerDrag, ControllerData, ExecuteEvents.initializePotentialDrag);
        }

        // PointerUp notification
        if (released)
        {
            // Debug.Log("Executing pressup on: " + pointer.pointerPress);
            ExecuteEvents.Execute(ControllerData.pointerPress, ControllerData, ExecuteEvents.pointerUpHandler);

            // see if we mouse up on the same element that we clicked on...
            var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

            // PointerClick and Drop events
            if (ControllerData.pointerPress == pointerUpHandler && ControllerData.eligibleForClick)
            {
                ExecuteEvents.Execute(ControllerData.pointerPress, ControllerData, ExecuteEvents.pointerClickHandler);
            }
            else if (ControllerData.pointerDrag != null && ControllerData.dragging)
            {
                ExecuteEvents.ExecuteHierarchy(currentOverGo, ControllerData, ExecuteEvents.dropHandler);
            }

            ControllerData.eligibleForClick = false;
            ControllerData.pointerPress = null;
            ControllerData.rawPointerPress = null;

            if (ControllerData.pointerDrag != null && ControllerData.dragging)
                ExecuteEvents.Execute(ControllerData.pointerDrag, ControllerData, ExecuteEvents.endDragHandler);

            ControllerData.dragging = false;
            ControllerData.pointerDrag = null;

            if (ControllerData.pointerDrag != null)
                ExecuteEvents.Execute(ControllerData.pointerDrag, ControllerData, ExecuteEvents.endDragHandler);

            ControllerData.pointerDrag = null;

            // send exit events as we need to simulate this on touch up on touch device
            ExecuteEvents.ExecuteHierarchy(ControllerData.pointerEnter, ControllerData, ExecuteEvents.pointerExitHandler);
            ControllerData.pointerEnter = null;
        }
    }

    private bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
    {
        if (!useDragThreshold)
            return true;

        //this always returns false if override pointereventdata in curveduiraycster.cs is set to false. There is no past pointereventdata to compare with then.
        return (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
    }

    /// <summary>
    /// Force selection of a gameobject.
    /// </summary>
    private void Select(GameObject go)
    {
        ClearSelection();
        if (ExecuteEvents.GetEventHandler<ISelectHandler>(go))
        {
            eventSystem.SetSelectedGameObject(go);
        }
    }

    /// <summary>
    /// Adds necessary components to Vive controller gameobjects. These will let us know what inputs are used on them.
    /// </summary>
    private void SetupViveControllers()
    {
        //Find Controller manager on the scene.
        if (controllerManager == null)
        {
            SteamVR_ControllerManager[] potentialManagers = Object.FindObjectsOfType<SteamVR_ControllerManager>();
            controllerManager = null;

            //ignore external camera created by externalcamera.cfg for mixed reality videos
            if (potentialManagers.GetLength(0) > 0)
            {
                for (int i = 0; i < potentialManagers.GetLength(0); i++)
                {
                    if (potentialManagers[i].gameObject.name != "External Camera")
                        controllerManager = potentialManagers[i];
                }
            }

            if (controllerManager == null)
                Debug.LogError("Can't find SteamVR_ControllerManager on scene. It is required to use VIVE control method. Make sure all SteamVR prefabs are present.");
        }
#endif
    }
#endregion



#region EVENT PROCESSING - OCULUS TOUCH
    protected virtual void ProcessOculusTouchController()
    {
#if CURVEDUI_TOUCH
        //First pass the direction and position of the controller as ray to your canvas
        CustomControllerRay = new Ray(TouchControllerTransform.position, TouchControllerTransform.forward);

        //now pass the state of your button to CurvedUIInputModule
        CustromControllerButtonDown = OVRInput.Get(InteractionButton, OVRInput.Controller.RTouch);

        ProcessCustomRayController();
#endif
    }
#endregion



#region EVENT PROCESSING - CUSTOM RAY
    protected virtual void ProcessCustomRayController(){

		var mouseData = GetMousePointerEventData(0);
		PointerEventData eventData = mouseData.GetButtonState(PointerEventData.InputButton.Left).eventData.buttonData;

		if(pressedDown && !pressedLastFrame){//pointer down interactions
			GameObject currentOverGo = eventData.pointerCurrentRaycast.gameObject;

			eventData.eligibleForClick = true;
			eventData.delta = Vector2.zero;
			eventData.dragging = false;
			eventData.useDragThreshold = true;
			eventData.pressPosition = eventData.position;
			eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;

			DeselectIfSelectionChanged(currentOverGo, eventData);

			if (eventData.pointerEnter != currentOverGo)
			{
				// send a pointer enter to the touched element if it isn't the one to select...
				HandlePointerExitAndEnter(eventData, currentOverGo);
				eventData.pointerEnter = currentOverGo;
			}

			// search for the control that will receive the press
			// if we can't find a press handler set the press
			// handler to be what would receive a click.
			var newPressed = ExecuteEvents.ExecuteHierarchy(currentOverGo, eventData, ExecuteEvents.pointerDownHandler);

			// didnt find a press handler... search for a click handler
			if (newPressed == null)
				newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

			// Debug.Log("Pressed: " + newPressed);

			float time = Time.unscaledTime;

			if (newPressed == eventData.lastPress)
			{
				var diffTime = time - eventData.clickTime;
				if (diffTime < 0.3f)
					++eventData.clickCount;
				else
					eventData.clickCount = 1;

				eventData.clickTime = time;
			}
			else
			{
				eventData.clickCount = 1;
			}

			eventData.pointerPress = newPressed;
			eventData.rawPointerPress = currentOverGo;

			eventData.clickTime = time;

			// Save the drag handler as well
			eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);

			if (eventData.pointerDrag != null)
				ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);

		} else if(!pressedDown && pressedLastFrame){//pointer up interactions


			//if we did not move the pointer since the begining, this is a click.
			if (eventData.pointerPress == eventData.selectedObject/*Vector2.Distance (eventData.position, eventData.pressPosition) < dragThreshold*/) {
				ExecuteEvents.Execute (eventData.selectedObject, eventData, ExecuteEvents.pointerClickHandler);
			} 
          
			//execute pointer up events
			ExecuteEvents.Execute(eventData.selectedObject, eventData, ExecuteEvents.pointerUpHandler);

			//process end drag
			//if (eventData.pointerDrag != null && eventData.dragging) {
				ExecuteEvents.Execute (eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
				eventData.dragging = false;
				eventData.pointerDrag = null;
			//}
		}


		if (eventData.IsPointerMoving ()) {
			ProcessDrag (eventData);
			ProcessMove (eventData);
		}

		//save button state for this frame
		pressedLastFrame = pressedDown;
	}
    #endregion


    #region PUBLIC FUNCTIONS
    ///// <summary>
    ///// Clear the currently selected gameobject
    ///// </summary>
    //public void ClearSelection()
    //{
    //    if (eventSystem.currentSelectedGameObject)
    //    {
    //        eventSystem.SetSelectedGameObject(null);
    //    }
    //}

    static T EnableInputModule<T>() where T : BaseInputModule
    {
        bool moduleMissing = true;
        //GameObject eventGO = EventSystem.current.gameObject;
        EventSystem eventGO = GameObject.FindObjectOfType<EventSystem>();
        foreach (BaseInputModule module in eventGO.GetComponents<BaseInputModule>())
        {
            if (module is T)
            {
                moduleMissing = false;
                module.enabled = true;
            }
            else
                module.enabled = false;
        }

        if (moduleMissing)
            eventGO.gameObject.AddComponent<T>();

        return eventGO.GetComponent<T>();
    }
#endregion



#region SETTERS AND GETTERS

    /// <summary>
    /// When in CUSTOM_RAY controller mode, RayCaster will use this worldspace Ray to determine which Canvas objects are being selected.
    /// </summary>
    public static Ray CustomControllerRay {
        get { return Instance.customControllerRay; }
        set  {  Instance.customControllerRay = value;  }
    }

    /// <summary>
    /// When in CUSTOM_RAY controller mode, Input module will use this wbool to determine whether interaction button is pressed.
    /// </summary>
    public static bool CustromControllerButtonDown {
        get { return Instance.pressedDown; }
        set { Instance.pressedDown = value; }

    }

    /// <summary>
    /// Returns the position of the world space pointer in Canvas' local space. 
    /// You can use it to position an image on world space mouse pointer's position.
    /// </summary>
    public Vector2 WorldSpaceMouseInCanvasSpace {
        get { return worldSpaceMouseInCanvasSpace; }
        set
        {
            worldSpaceMouseInCanvasSpace = value;
            lastWorldSpaceMouseOnCanvas = value;
        }
    }

    /// <summary>
    /// The change in position of the world space mouse in canvas' units.
    /// Counted since the last frame.
    /// </summary>
    public Vector2 WorldSpaceMouseInCanvasSpaceDelta {
        get { return worldSpaceMouseInCanvasSpace - lastWorldSpaceMouseOnCanvas; }
    }

    /// <summary>
    /// How many units in Canvas space equals one unit in screen space.
    /// </summary>
    public float WorldSpaceMouseSensitivity {
        get { return worldSpaceMouseSensitivity; }
        set { worldSpaceMouseSensitivity = value; }
    }


    /// <summary>
    /// Current controller mode. Decides how user can interact with the canvas. 
    /// </summary>
    public static CurvedUIController Controller {
        get { return Instance.controller; }
        set
        {
            if (Instance.controller != value)
            {   Instance.controller = value;  }
        }
    }

    /// <summary>
    /// Gameobject we're currently pointing at.
    /// </summary>
    public GameObject CurrentPointedAt {
        get { return currentPointedAt; }
    }

    /// <summary>
    /// Which Controller can be used to interact with canvas. Left, Right or Both. Default Right.
    /// </summary>
    public ActiveVRController UsedVRController {
        get { return eventController; }
        set { eventController = value;  }
    }

#if CURVEDUI_VIVE
        /// <summary>
        /// Scene's controller manager. Used to get references for Vive controllers.
        /// </summary>
        public SteamVR_ControllerManager SteamVRControllerManager {
            get { return steamVRControllerManager; }
            set
            {
                if (steamVRControllerManager != value)  {
                    steamVRControllerManager = value;
                }
            }
        }

        /// <summary>
        /// Get or Set controller manager used by this input module.
        /// </summary>
        public SteamVR_ControllerManager ControllerManager {
            get { return controllerManager; }
            set
            {
                controllerManager = value;
                SetupViveControllers();
            }
        }
   
        /// <summary>
        /// Returns Right Vive Controller. Ask this component for any button states.;
        /// </summary>
        public static CurvedUIViveController Right {
            get {

                if (!rightCont)
                 rightCont = controllerManager.right.AddComponentIfMissing<CurvedUIViveController>();

                return rightCont ; }
        }

        /// <summary>
        /// Returns Left Vive Controller. Ask this component for any button states.;
        /// </summary>
        public static CurvedUIViveController Left {
            get {

                if (!leftCont)
                  leftCont = controllerManager.left.AddComponentIfMissing<CurvedUIViveController>();

                return leftCont; }
        }

      
#endif


#if CURVEDUI_TOUCH
    public OVRInput.Button OculusTouchInteractionButton {
        get { return InteractionButton; }
        set
        {
            InteractionButton = value;
        }
    }
#endif

#endregion
}
