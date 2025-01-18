using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [Header("Rerences")]
    [SerializeField] SaveGameData gameData;
    [SerializeField] CameraCTR camVievSettins;
    [SerializeField] SwitchScreenOnPress switchScreens;
    [SerializeField] [Tooltip("Get the main UI here that is not on player!")]GameObject UserInterface;
    [SerializeField] PlayerBaseMovemant movemant;
    [SerializeField] Slider strengthMeter;
    public Transform playerHand;
    public Item itemEquiped;
    private bool isAiming;
    bool menuActive;
    public GameObject meter;

    public float speedMeter = 1f; // Speed of movement
    private bool movingRight = true; // Direction flag

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AlertDebug();
        menuActive = false;
        isAiming = false;
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
        if (Input.GetKeyDown(gameData.playerPrefs.aim) && itemEquiped.itemType == Item.ItemType.throwable)
        {
            Debug.Log("inAimMode");
            Throwable();
            isAiming = true;
            Flipping();
    
        }
        else
        {
            Debug.Log("stop aiming");
            isAiming = false;
        }
        if(Input.GetKeyDown(gameData.playerPrefs.Throw) && isAiming)
        {
           
            Throwable();
        }
    }
    void Flipping()
    {
        if (Input.GetKeyDown(gameData.playerPrefs.aim))
        {

        }
    }
    void Throwable()
    {
        if (playerHand.childCount > 0)
        {
            GameObject throwable = playerHand.GetChild(0).gameObject;
            Rigidbody bullet = throwable.GetComponent<Rigidbody>();
            if (bullet != null)
            {
                bullet.isKinematic = false;
                bullet.AddForce(playerHand.forward * strengthMeter.value, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Throwable object has no Rigidbody component.");
            }
        }
        else
        {
            Debug.LogWarning("No throwable item found in playerHand.");
            
        }

    }

    void Meter()
    {
        if (strengthMeter == null) return;

        // Move the slider value
        if (movingRight)
        {
            strengthMeter.value += speedMeter * Time.deltaTime;
            if (strengthMeter.value >= strengthMeter.maxValue)
                movingRight = false; // Reverse direction
        }
        else
        {
            strengthMeter.value -= speedMeter * Time.deltaTime;
            if (strengthMeter.value <= strengthMeter.minValue)
                movingRight = true; // Reverse direction
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
