using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public bool debugOculusOverride;
    public KeyCode recenterOculusKey = KeyCode.R;

    private bool usingOculus;
    private GameObject centerEyeCam;
    private GameObject leftEyeAnchor;
    private GameObject rightEyeAnchor;
    private GameObject ovrManager;

    // accessor method for sharing current oculus state
    public bool UsingOculus { get; set; }

    //called after awake and before start
    void OnEnable()
    {
        // subscribe to the OVRManager events so that we can run a function whenether the HMD is acquired or lost
        OVRManager.HMDAcquired += DetectUsingOculus;
        OVRManager.HMDLost += DetectUsingOculus;
    }

	// Use this for initialization
	void Start () {
        usingOculus = OVRManager.display.isPresent;
        centerEyeCam = transform.FindChild("CenterEyeAnchor/Camera").gameObject;
        rightEyeAnchor = transform.FindChild("RightEyeAnchor").gameObject;
        leftEyeAnchor = transform.FindChild("LeftEyeAnchor").gameObject;
        ovrManager = transform.FindChild("OVRManager").gameObject;

        // perform actions on start
        DetectUsingOculus();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(recenterOculusKey))
            RecenterOculusPos();

	}

    // called on frame when script is disabled
    void OnDisable()
    {
        OVRManager.HMDAcquired -= DetectUsingOculus;
        OVRManager.HMDLost -= DetectUsingOculus;
    }


    //
    // Supporting Methods
    //

    // detects whether the player is using the oculus
    void DetectUsingOculus()
    {
        // sets local usingOculus to whether there are HMDs detected
        usingOculus = Ovr.Hmd.Detect() > 0;
        // allow override for debugging
        if (debugOculusOverride)
            usingOculus = true;
        // only activate OVRManager if you are using the oculus
        ovrManager.SetActive(usingOculus);
        // perform code to switch between oculus and pc player
        PlayerCamSwitch();
    }


    // switches camera from oculus mode to regular 3rd person based on whether the oculus is detected
    void PlayerCamSwitch()
    {
        if (usingOculus)
        {
            centerEyeCam.SetActive(false);
            leftEyeAnchor.SetActive(true);
            rightEyeAnchor.SetActive(true);
            //currentMenu.transform.parent = player.transform;
        }
        else
        {
            leftEyeAnchor.SetActive(false);
            rightEyeAnchor.SetActive(false);
            centerEyeCam.SetActive(true);
            //currentMenu.transform.parent = player.transform.Find("Camera").transform;
        }
    }


    public void RecenterOculusPos()
    {
        if (usingOculus)
        {
            Debug.Log("Oculus Recentered.");
            OVRManager.display.RecenterPose();
        }
    }

    

}
