using UnityEngine;
using System.Collections;

public class PointTo : MonoBehaviour {

    public Transform lookAt;

	// Use this for initialization
	void Start () {
        this.transform.rotation = Quaternion.LookRotation(lookAt.position);
	}
	
}
