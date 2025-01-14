using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraCTR : MonoBehaviour
{
    //private
    float yInput;
    float xInput;
    float horizontalRotation;
    float verticalRotation;

    bool activeMouse;

    [Header("Refrences")]
    [SerializeField] SaveGameData saveGameData;
    [SerializeField] Transform playerOrientation;
    Camera playerCamera;

    [Header("Camera settings")]
    [Range(60, 110)] public int fieldOfView = 60;
    private float horizontalSensitivity;
    private float verticalSensitivity;

    [Header("Tweeks")]
    [SerializeField] float horizontalMultiplier = 100;
    [SerializeField] float verticalMultiplier = 100;
    void Start()
    {
        activeMouse = true;
        playerCamera = GetComponent<Camera>();
        ShowMouse();
    }

    void Update()
    {
        
        SetParamaters();
        if (Input.GetKeyDown(saveGameData.playerPrefs.activateMouse))
        {
            ShowMouse();
        }
        if (!activeMouse)
        {
            MouseInput();
        }
    }

    void MouseInput()
    {
        // get inputs
        yInput = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 2 * verticalMultiplier * verticalSensitivity;
        xInput = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 2 * horizontalMultiplier * horizontalSensitivity;

        InversionSystem();

        horizontalRotation = Mathf.Clamp(horizontalRotation, -90f,90f);

        transform.rotation = Quaternion.Euler(horizontalRotation, verticalRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, verticalRotation, 0);

    }
    public void SetParamaters()
    {
        playerCamera.fieldOfView = saveGameData.fieldOfView;
        verticalSensitivity = saveGameData.verticalSensitivity;
        horizontalSensitivity = saveGameData.horizontalSensitivity;
    }

    void InversionSystem()
    {
        if (saveGameData.camMoveTypes == SaveGameData.CameraMovemantType.InvertedAll)
        {
            horizontalRotation += yInput;
            verticalRotation -= xInput;
        }
        else if (saveGameData.camMoveTypes == SaveGameData.CameraMovemantType.InvertedVertical)
        {
            horizontalRotation -= yInput;
            verticalRotation -= xInput;
        }
        else if (saveGameData.camMoveTypes == SaveGameData.CameraMovemantType.InvertedHorizontal)
        {
            horizontalRotation += yInput;
            verticalRotation += xInput;
        }
        else if (saveGameData.camMoveTypes == SaveGameData.CameraMovemantType.Default)
        {
            horizontalRotation -= yInput;// inversion cuz unity sucs !
            verticalRotation += xInput;
        }
        else
        {
            Debug.LogWarning("Error in inversion System");
        }
    }

    void ShowMouse()
    {
        if (!activeMouse)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            activeMouse = true;
        }
        else if (activeMouse) 
        {            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            activeMouse = false;
        }
    }
}
