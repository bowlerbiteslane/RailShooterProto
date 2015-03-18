using UnityEngine;
using System.Collections;

public class MainMenu : VRGUI {

    public Texture startButtonTex;
    public Texture recenterButtonTex;

    public delegate void OnStartClicked();
    public static event OnStartClicked OnStartButtonClicked;

    public delegate void OnRecenterClicked();
    public static event OnRecenterClicked OnRecenterButtonClicked;


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
  

    public override void OnVRGUI()
    {
        GUILayoutOption buttonHeight = GUILayout.Height(Screen.height * .25f);
        GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
        GUILayout.Space(Screen.height * .25f);
        if (GUILayout.Button(startButtonTex, buttonHeight))
        {
            if (OnStartButtonClicked != null)
                OnStartButtonClicked();
        }
        if (GUILayout.Button(recenterButtonTex, buttonHeight))
        {
            if (OnRecenterButtonClicked != null)
                OnRecenterButtonClicked();
        }
        GUILayout.EndArea();
    }
}
