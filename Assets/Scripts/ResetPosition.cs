using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour {

    public KeyCode HMDResetKey = KeyCode.R;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(HMDResetKey))
        {
            OVRManager.display.RecenterPose();
        }
	}
}
