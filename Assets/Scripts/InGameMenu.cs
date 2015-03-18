using UnityEngine;
using System.Collections;

public class InGameMenu : VRGUI {

    public delegate void OnExitClicked();
    public static event OnExitClicked OnExitButtonClicked;

    public Texture exitButtonTex;
    void Start()
    {
        if (exitButtonTex == null)
        {
            Debug.Log("There is not start button texure associated with the Menu. Disabling Script.");
            this.enabled = false;
        }
    }
  

    public override void OnVRGUI()
    {
        GUILayoutOption buttonHeight = GUILayout.Height(Screen.height*.25f);
        GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
        GUILayout.Space(Screen.height*.25f);
        if (GUILayout.Button(exitButtonTex, buttonHeight))
        {
            if (OnExitButtonClicked != null)
                OnExitButtonClicked();
            Debug.Log("Start button clicked.");
        }
        GUILayout.EndArea();
    }
}
