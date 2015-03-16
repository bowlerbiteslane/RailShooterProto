using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public KeyCode hideShowMenuKey = KeyCode.Escape;
    public GameObject playerMenu;

    private bool showMenu = true;

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        if (hideShowMenuKey == null)
            Debug.Log("No key set to hide menu in the GameContoller's HideMenu Script. Game may not work as intended.");
        if (playerMenu == null)
            Debug.Log("No player menu added to Game Controller. Game may not work as intended.");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(hideShowMenuKey)){
            showMenu = !showMenu;
            playerMenu.SetActive(showMenu);
        }
	}
}
