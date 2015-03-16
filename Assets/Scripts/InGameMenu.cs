using UnityEngine;
using System.Collections;

public class InGameMenu : VRGUI {

    public Texture startButtonTex;
    public GameObject railmover;

    private bool startButtonClicked = false;


    void Start()
    {
        if (startButtonTex == null)
        {
            Debug.Log("There is not start button texure associated with the Menu. Disabling Script.");
            this.enabled = false;
        }
        if (railmover == null)
        {
            Debug.Log("There is no railmover associated with the Menu. Disabling Script.");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (startButtonClicked)
        {
            railmover.GetComponent<RailMover>().StartMoving();
            startButtonClicked = false;
            gameObject.SetActive(false);
        }
    }
  

    public override void OnVRGUI()
    {
        GUILayoutOption buttonHeight = GUILayout.Height(500f);
        GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
        GUILayout.Space(200f);
        if (GUILayout.Button(startButtonTex, buttonHeight))
        {
            Debug.Log("Start button clicked.");
            startButtonClicked = true;
        }
        GUILayout.EndArea();
    }
}
