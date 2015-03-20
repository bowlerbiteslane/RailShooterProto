using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    
    public KeyCode hideShowMenuKey = KeyCode.Escape;
    // scene objects

    // reference prefabs
    public GameObject playerPrefab;
    public GameObject customOculusCamPrefab;
    public GameObject startCamPrefab;
    public GameObject railMoverPrefab;
    public GameObject railNodeGroupPrefab;
    public GameObject mainMenuPrefab;
    public GameObject inGameMenuPrefab;
    // reference spawn points
    public Transform startCamSpawn;
    public Transform playerSpawn;
    public Transform railMoverSpawn;
    // manage instances of objects internally
    private GameObject customOculusCam;
    private GameObject player;
    private GameObject startCam;
    private GameObject railMover;
    private GameObject railNodeGroup;
    private GameObject currentMenu;
    private GameObject mainMenu;
    private GameObject inGameMenu;

    private bool showMenu = true;


	// Use this for initialization
	void OnEnable () {
        // check for prefab prerequisites
        if (customOculusCamPrefab == null)
        {
            Debug.Log("No start cam in the GameContoller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (playerPrefab == null)
        {
            Debug.Log("No player prefab added to Game Controller. Game may not work as intended. Disabling Script.");
            this.enabled = false;
        }
        if (startCamPrefab == null)
        {
            Debug.Log("No startcam prefab in the GameContoller. Game may not work as intended. Disabling Script.");
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
        // instantiate gameobjects in start screen
        startCam = Instantiate(startCamPrefab, startCamSpawn.position, startCamSpawn.rotation) as GameObject;
        customOculusCam = Instantiate(customOculusCamPrefab, startCamSpawn.position, startCamSpawn.rotation) as GameObject;
        mainMenu = Instantiate(mainMenuPrefab, startCam.transform.position, startCam.transform.rotation) as GameObject;
        railMover = Instantiate(railMoverPrefab, railMoverSpawn.position, railMoverSpawn.rotation) as GameObject;
        railNodeGroup = Instantiate(railNodeGroupPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        railMover.GetComponent<RailMover>().railNodeGroup = railNodeGroup;
        railMover.GetComponent<RailMover>().rider = startCam.transform;
        // instantiate objects that aren't yet being used
        inGameMenu = Instantiate(inGameMenuPrefab, playerSpawn.position, playerSpawn.rotation) as GameObject;
        player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation) as GameObject;

        MainMenu.OnStartButtonClicked += StartGame;
        MainMenu.OnRecenterButtonClicked += customOculusCam.GetComponentInChildren<CameraManager>().RecenterOculusPos;
        InGameMenu.OnExitButtonClicked += EndGame;
        
    }

    // subscribe to custom menu events
    void Start()
    {
        // disable objects that aren't yet being used
        player.SetActive(false);
        inGameMenu.SetActive(false);

        Screen.showCursor = false;
        LoadStartScreen();
    }


    void Update()
    {
        if (Input.GetKeyDown(hideShowMenuKey))
            ToggleMenu();
    }

    // unsubscribe when script is disabled
    void OnDisable()
    {
        MainMenu.OnStartButtonClicked -= StartGame;
        MainMenu.OnRecenterButtonClicked -= customOculusCam.GetComponentInChildren<CameraManager>().RecenterOculusPos;
        InGameMenu.OnExitButtonClicked -= EndGame;
        
    }


    //
    // Support Methods
    //
    void LoadStartScreen()
    {
        startCam.SetActive(true);
        customOculusCam.SetActive(true);
        ParentAndMatchTransform(customOculusCam, startCam); // move cam before deactivating player -- otherwise oculus locks up
        player.SetActive(false);
        currentMenu = mainMenu;
        ParentAndMatchTransform(currentMenu, startCam);
        railMover.SetActive(true);
    }

    void StartGame()
    {
        //switch player to ground object
        player.SetActive(true);
        ParentAndMatchTransform(customOculusCam, player);   // move cam before deactivating startCam -- otherwise oculus locks up
        currentMenu = inGameMenu;
        startCam.SetActive(false);
        ParentAndMatchTransform(currentMenu, player);
        ShowMenu(false);
        railMover.SetActive(false);
    }

    void EndGame()
    {
        LoadStartScreen();
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void ShowMenu(bool show)
    {
        showMenu = show;
        currentMenu.SetActive(showMenu);
    }

    void ToggleMenu()
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

    void ParentAndMatchTransform(GameObject obj1, GameObject obj2)
    {
        //obj1.transform.position = obj2.transform.position;
        //obj1.transform.rotation = obj2.transform.rotation;
        obj1.transform.parent = obj2.transform;
        obj1.transform.localPosition = Vector3.zero;
        obj1.transform.localRotation = Quaternion.identity;
    }
    
}
