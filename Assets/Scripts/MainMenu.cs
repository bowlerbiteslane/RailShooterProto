using UnityEngine;
using System.Collections;

public class MainMenu : VRGUI {

    public Texture startButtonTex;
    public Texture recenterButtonTex;

    private bool startButtonClicked = false;
    private bool recenterButtonClicked = false;


    void Start()
    {
        if (startButtonTex == null)
        {
            Debug.Log("There is not start button texure associated with the Menu. Disabling Script.");
            this.enabled = false;
        }
        if (recenterButtonTex == null)
        {
            Debug.Log("There is no recenter button texure associated with the Menu. Disabling Script.");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (startButtonClicked)
        {
            //Application.LoadLevel(1);
            startButtonClicked = false;
        }
        if (recenterButtonClicked)
        {
            OVRManager.display.RecenterPose();
            recenterButtonClicked = false;
        }
    }
  

    public override void OnVRGUI()
    {
        GUILayoutOption buttonHeight = GUILayout.Height(Screen.height * .25f);
        GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
        GUILayout.Space(Screen.height * .25f);
        if (GUILayout.Button(startButtonTex, buttonHeight))
        {
            Debug.Log("Start button clicked.");
            startButtonClicked = true;
        }
        if (GUILayout.Button(recenterButtonTex, buttonHeight))
        {
            Debug.Log("Recenter button clicked.");
            recenterButtonClicked = true;
        }
        GUILayout.EndArea();
    }
}
