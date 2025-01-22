using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [Header("Rerences")]
    PlayerRefrences REFERENCE;
    [SerializeField] float strengthMeter;// well see this is a SLider
    public Item itemEquiped;
    private bool isAiming;
    bool menuActive;
    public GameObject meter;
    public Transform playerHand;


    public float speedMeter = 1f; // Speed of movement
    private bool movingRight = true; // Direction flag

    void Start()
    {
        REFERENCE = GetComponent<PlayerRefrences>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuActive = false;
        isAiming = false;
        playerHand = REFERENCE.playerHand;
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

        if(Input.GetKeyDown(REFERENCE.inputKeys.aim) && itemEquiped == null)
        {
            Flipping();
        }
        // firemehanism
        else if (Input.GetKeyDown(REFERENCE.inputKeys.aim) && itemEquiped.itemType == Item.ItemType.Throwable)
        {
            Debug.Log("inAimMode");
            isAiming = true;
            REFERENCE.strengthSlider.GetComponent<StrengthIndicator>().isActive = true;
            REFERENCE.dangerSlider.GetComponent<StrengthIndicator>().isActive = true;
        }
        else if (Input.GetKeyUp(REFERENCE.inputKeys.aim) && isAiming)
        {
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
  /*  public void EquipItem(Transform newGameObject)
    {
        Debug.Log("Equipping");

        // Check if there are no children in the player's hand
        if (REFERENCE.playerHand.childCount == 0)
        {
            // Instantiate the new item in the player's hand
            Transform Item = Instantiate(newGameObject, REFERENCE.playerHand);
            REFERENCE.playerHand.GetChild(0).position = new Vector3(0,0,0);

            // Equip the new item
            itemEquiped = Item.GetComponent<Item>();

            // Debug: Log the new item that has been equipped
            Debug.Log($"New item equipped: {Item.name}");
        }
        else
        {
            Debug.LogWarning("Player hand is not empty. Can't equip a new item.");
        }
    }*/
    void Throwable()
    {
        if (REFERENCE.playerHand.childCount > 0)
        {
            Debug.Log("Thrown item 0");
            GameObject throwable = playerHand.GetChild(0).gameObject;
            Rigidbody bullet = throwable.GetComponent<Rigidbody>();
            if (bullet != null)
            {
                bullet.isKinematic = false;
                bullet.AddForce(playerHand.forward * REFERENCE.strengthSlider.value, ForceMode.Impulse);
                REFERENCE.strengthSlider.GetComponent<StrengthIndicator>().isActive = false;
                REFERENCE.dangerSlider.GetComponent<StrengthIndicator>().isActive = false;
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
/*
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
    }*/
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
