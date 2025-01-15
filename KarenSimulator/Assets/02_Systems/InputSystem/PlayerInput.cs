using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Rerences")]
    [SerializeField] SaveGameData gameData;
    [SerializeField] CameraCTR camVievSettins;
    [SerializeField] SwitchScreenOnPress switchScreens;
    [SerializeField] [Tooltip("Get the main UI here that is not on player!")]GameObject UserInterface;
    [SerializeField] PlayerBaseMovemant movemant;
    [SerializeField] Transform playerHand;

    bool menuActive = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AlertDebug();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(gameData.playerPrefs.menu))
        {
            //set menu active
            if (!menuActive)
            {
                menuActive = true;
                movemant.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                menuActive = false;
                movemant.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }// menu activation
        if (!menuActive)
        {
            InputReader();
        }
        
    }

    void InputReader()
    {
        if (Input.GetKeyDown(gameData.playerPrefs.screenSwitch))
        {
            ScreanManaging();
        }


        // firemehanism
        if (Input.GetKeyDown(gameData.playerPrefs.attack))
        {
            //attack function so for now its a tgrowable item
            // get child Item script compare enum type tghrowable, consuable ... request an apropriate action.
            playerHand.GetChild(0);// use item function her --> (0).sjdadj
        }

    }
    void ScreanManaging()
    {
        switchScreens.SwitchScreen();

        //activate the top down view
        if (switchScreens.POV == true)
        {
            camVievSettins.firstPersonView = true;
            movemant.fps = true;
        }
        else
        {
            camVievSettins.firstPersonView = false;
            movemant.ResetOrientation();
            movemant.fps = false;
        }
    }

    void AlertDebug()
    {
        if (UserInterface == null)
        {
            Debug.LogWarning("User Interface not assigned");
        }

        if (gameData == null)
        {
            Debug.LogWarning("GameData (SaveGameData) not assigned");
        }

        if (camVievSettins == null)
        {
            Debug.LogWarning("Camera View Settings (CameraCTR) not assigned");
        }

        if (switchScreens == null)
        {
            Debug.LogWarning("SwitchScreens not assigned");
        }

        if (movemant == null)
        {
            Debug.LogWarning("Movement (PlayerBaseMovemant) not assigned");
        }

        if (playerHand == null)
        {
            Debug.LogWarning("Player Hand not assigned");
        }
    }
}
