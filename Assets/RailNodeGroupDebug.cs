using UnityEngine;
using System.Collections;

public class RailNodeGroupDebug : MonoBehaviour {
    
    public bool loop;

    private Transform[] railNodes;
    
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
