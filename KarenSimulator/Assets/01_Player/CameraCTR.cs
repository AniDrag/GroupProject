using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraCTR : MonoBehaviour
{
    //private tracks ouse inputs
    float yInput;
    float xInput;
    float horizontalRotation;
    float verticalRotation;

    public bool firstPersonView;

    [Header("Refrences")]
    [Tooltip("Refrence is curent save we are on, on the player a child orientation")]
    [SerializeField] SaveGameData saveGameData;
    [SerializeField] Transform playerOrientation;
    [SerializeField] Transform thirdPersonTracker;
    Camera playerCamera;

    [Header("Camera settings")]
    [Range(60, 110)] public int fieldOfView = 60;
    private float horizontalSensitivity;
    private float verticalSensitivity;

    [Header("Tweeks")]
    [Tooltip("This is a debug setting,")]
    [SerializeField] float horizontalMultiplier = 100;
    [SerializeField] float verticalMultiplier = 100;
    void Start()
    {
        firstPersonView = true;
        playerCamera = GetComponent<Camera>();
        SetParamaters();
    }

    void Update()
    {
        
        if (firstPersonView)
        {
            MouseInput();
        }
        else
        {
           THirdPersonControls();
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

    void THirdPersonControls()
    {
        transform.rotation = Quaternion.Euler(0, thirdPersonTracker.rotation.y, 0);
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
}
