using UnityEngine;
using System.Collections;

public class RailMover : MonoBehaviour {

    public Transform rider;
    public int startNode = 1;
    public float railSpeed = 5f;
    public float rotationSmoothing = .5f;
    public bool movingOnStart = true;
    public bool loop = true;

    private Transform[] railNodes;
    private int nodeIter;                                   
    private bool moving = false;

	// Use this for initialization
	void Start () {
        if (rider == null)
        {
            Debug.Log("No player attached to RailMover Script. Disabling Script.");
            this.enabled = false;
        }
        railNodes = this.GetComponentsInChildren<Transform>(); // will return parent transform at railNodes[0]
        if (railNodes.Length == 0)
        {
            Debug.Log("No railnodes nested under the railmover. Disabling Script.");
            this.enabled = false;
        }
        nodeIter = Mathf.Clamp(startNode, 1, railNodes.Length - 1);             // initialize as one so that we skip over the parent transform at (0,0,0), ensures that value does not exceed array length  
        moving = movingOnStart;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //moving = !moving;
            StartMoving();
        }
        if (moving)
        {
            // move rider toward the currently indexed node's position based on railSpeed
            rider.position = Vector3.MoveTowards(rider.position, railNodes[nodeIter].position, railSpeed * Time.deltaTime);
            
            // rotate rider toward the currently indexed node's position
            Quaternion targetRotatation = Quaternion.LookRotation(railNodes[nodeIter].position - rider.position);                   // get the target rotation 
            
            // make the smoothing value consistent and not exceed 1 as it will represent a percent distance between two rotations
            float smoothing = Mathf.Min(rotationSmoothing * Time.deltaTime, 1);
            
            // lerp the rider's rotation toward the target rotation
            rider.rotation = Quaternion.Lerp(rider.rotation, targetRotatation, smoothing);
            if (rider.position == railNodes[nodeIter].position)
            {
                if (nodeIter < railNodes.Length - 1){
                    nodeIter++;
                }
                else
                {
                    if (loop)
                        nodeIter = 1;
                    Debug.Log("Reached the last rail node.");
                } 
            }
        }
	}

    public void StartMoving()
    {
        moving = true;
    }

    void OnDrawGizmos()
    {
        railNodes = this.GetComponentsInChildren<Transform>(); // will return parent transform at railNodes[0]
        for (int i = 1; i < railNodes.Length; i++)
        {
            if (i < railNodes.Length - 1)
                Debug.DrawLine(railNodes[i].position, railNodes[i + 1].position, Color.red);
            else if (loop)
                Debug.DrawLine(railNodes[i].position, railNodes[1].position, Color.red);
        }
        
    }
}
