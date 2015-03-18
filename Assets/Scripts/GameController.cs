using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    
    public KeyCode hideShowMenuKey = KeyCode.Escape;
    public bool debugOculusOverride = false;
    // scene objects
    public GameObject ovrManager;

    // reference prefabs
    public GameObject startCamPrefab;
    public GameObject playerPrefab;
    public GameObject railMoverPrefab;
    public GameObject railNodeGroupPrefab;
    public GameObject mainMenuPrefab;
    public GameObject inGameMenuPrefab;
    // reference spawn points
    public Transform startCamSpawn;
    public Transform playerSpawn;
    public Transform railMoverSpawn;
    // manage instances of objects internally
    private GameObject player;
    private GameObject railMover;
    private GameObject railNodeGroup;
    private GameObject currentMenu;

    private bool usingOculus = false;
    private bool showMenu = false;


	// Use this for initialization
	void Start () {
        // check for prefab prerequisites
        if (ovrManager == null)
        {
            Debug.Log("No ovrManager in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (startCamPrefab == null)
        {
            Debug.Log("No start cam in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (playerPrefab == null)
        {
            Debug.Log("No player prefab added to Game Controller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (railMoverPrefab == null)
        {
            Debug.Log("No railmover prefab in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (railNodeGroupPrefab == null)
        {
            Debug.Log("No railNodeGroup prefab in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (mainMenuPrefab == null)
        {
            Debug.Log("No mainmenu prefab in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (inGameMenuPrefab == null)
        {
            Debug.Log("No mainmenu prefab in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        // check for spawn transform prerequisites
        if (playerSpawn == null)
        {
            Debug.Log("No player spawn added to Game Controller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (startCamSpawn == null)
        {
            Debug.Log("No player prefab added to Game Controller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (railMoverSpawn == null)
        {
            Debug.Log("No railmover prefab in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        // perform actions on start
        DetectUsingOculus();
        Screen.showCursor = false;
        LoadStartScreen();
    }

    // subscribe to custom menu events
    void OnEnable()
    {
        MainMenu.OnStartButtonClicked += StartGame;
        MainMenu.OnRecenterButtonClicked += RecenterOculus;
        InGameMenu.OnExitButtonClicked += EndGame;
        // subscribe to the OVRManager events so that we can run a function whenether the HMD is acquired or lost
        OVRManager.HMDAcquired += DetectUsingOculus;
        OVRManager.HMDLost += DetectUsingOculus;
    }
    // unsubscribe when script is disabled
    void OnDisable()
    {
        MainMenu.OnStartButtonClicked -= StartGame;
        MainMenu.OnRecenterButtonClicked -= RecenterOculus;
        InGameMenu.OnExitButtonClicked -= EndGame;
        OVRManager.HMDAcquired -= DetectUsingOculus;
        OVRManager.HMDLost -= DetectUsingOculus;
    }


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
        if (player != null)
        {
            PlayerCamSwitch();
        }
    }

    void PlayerCamSwitch()
    {
        if (usingOculus){
            player.transform.FindChild("Camera").gameObject.SetActive(false);
            currentMenu.transform.parent = player.transform;
        }  
        else{
            player.transform.FindChild("OVRCameraRig").gameObject.SetActive(false);
            currentMenu.transform.parent = player.transform.Find("Camera").transform;
        }  
    }

    
    void LoadStartScreen()
    {
        player = Instantiate(startCamPrefab, startCamSpawn.position, startCamSpawn.rotation) as GameObject;
        currentMenu = Instantiate(mainMenuPrefab, player.transform.position, player.transform.rotation) as GameObject;
        PlayerCamSwitch(); // switch after player and menu are instantiated
        railMover = Instantiate(railMoverPrefab, railMoverSpawn.position, railMoverSpawn.rotation) as GameObject;
        railNodeGroup = Instantiate(railNodeGroupPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        railMover.GetComponent<RailMover>().railNodeGroup = railNodeGroup;
        railMover.GetComponent<RailMover>().rider = player.transform;
    }

    void StartGame()
    {
        //switch player to ground object
        Destroy(player);    // destroys previous player object
        player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation) as GameObject;
        currentMenu = Instantiate(inGameMenuPrefab, player.transform.position, player.transform.rotation) as GameObject;
        PlayerCamSwitch(); // switch after player and menu are instantiated
        ShowMenu(false);
        // destroy startmenu
        Destroy(railMover);
        Debug.Log("Game Started.");
    }

    void EndGame()
    {
        if (player != null)
            Destroy(player);
        LoadStartScreen();
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void HideShowMenu()
    {
        if (currentMenu != null)
        {
            showMenu = !showMenu;
            currentMenu.SetActive(showMenu);
        }
        else
        {
            Debug.Log("Referenced a null menu object.");
        }
    }

    void ShowMenu(bool show)
    {
        showMenu = show;
        currentMenu.SetActive(showMenu);
    }

    void RecenterOculus()
    {
        Debug.Log("Oculus Recentered.");
        OVRManager.display.RecenterPose();
    }


    void Update()
    {
        if (Input.GetKeyDown(hideShowMenuKey))
            HideShowMenu();
    }
    
}
