using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerInput : MonoBehaviour
{
    [Header("Rerences")]
    PlayerRefrences REFERENCE;
    [SerializeField] Slider strengthMeter;
    private Item itemEquiped;
    private bool isAiming;
    bool menuActive;
    public GameObject meter;

    public float speedMeter = 1f; // Speed of movement
    private bool movingRight = true; // Direction flag

    void Start()
    {
        REFERENCE = GetComponent<PlayerRefrences>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuActive = false;
        isAiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(REFERENCE.inputKeys.menu))
        {
            //set menu active
            if (!menuActive)
            {
                menuActive = true;
                REFERENCE.playerMovemant.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                menuActive = false;
                REFERENCE.playerMovemant.enabled = true;
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
        if (Input.GetKeyDown(REFERENCE.inputKeys.screenSwitch))
        {
            ScreanManaging();
        }


        // firemehanism
        if (Input.GetKeyDown(REFERENCE.inputKeys.aim) && itemEquiped.itemType == Item.ItemType.Throwable)
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
        if(Input.GetKeyDown(REFERENCE.inputKeys.throwObject) && isAiming)
        {
           
            Throwable();
        }
    }
    void Flipping()
    {
        if (Input.GetKeyDown(REFERENCE.inputKeys.aim))
        {

        }
    }
    public void EquipItem(GameObject newGameObject)
    {
        // Check if there is more than 1 child in the player's hand
        if (REFERENCE.playerHand.childCount > 1)
        {
            // Debug: Log that we are replacing the current item
            Debug.Log("Replacing the currently equipped item.");

            // get player position and drop item
            Vector3 dropPoint = transform.position;

            // Spawn the current item on the ground at the player's position
            Instantiate(REFERENCE.playerHand.GetChild(0), dropPoint, Quaternion.identity);

            // Destroy the current item from the player's hand
            Destroy(REFERENCE.playerHand.GetChild(0));

            // Debug: Log the successful replacement
            Debug.Log("Current item dropped and destroyed.");
        }

        Instantiate(newGameObject, REFERENCE.playerHand);
        // Equip the new item
        itemEquiped = newGameObject.GetComponent<Item>();

        // Debug: Log the new item that has been equipped
        Debug.Log($"New item equipped: {newGameObject.name}");
    }
    void Throwable()
    {
        if (REFERENCE.playerHand.childCount > 0)
        {
            GameObject throwable = REFERENCE.playerHand.GetChild(0).gameObject;
            Rigidbody bullet = throwable.GetComponent<Rigidbody>();
            if (bullet != null)
            {
                bullet.isKinematic = false;
                bullet.AddForce(REFERENCE.playerHand.forward * strengthMeter.value, ForceMode.Impulse);
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
        REFERENCE.switchScreens.SwitchScreen();

        //activate the top down view
        if (REFERENCE.switchScreens.isFirstPersonView == true)
        {
            REFERENCE.cammeraControler.firstPersonView = true;
            REFERENCE.playerMovemant.fps = true;
        }
        else
        {
            REFERENCE.cammeraControler.firstPersonView = false;
            REFERENCE.playerMovemant.ResetOrientation();
            REFERENCE.playerMovemant.fps = false;
        }
    }
}
