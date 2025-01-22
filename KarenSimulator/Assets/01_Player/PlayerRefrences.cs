using UnityEngine;
using UnityEngine.UI;

public class PlayerRefrences : MonoBehaviour
{
    [Header("OBJs")]
    public SaveGameData gameData;
    public KeyBinds inputKeys;

    [Header("Player body Children")]
    public PlayerBaseMovemant playerMovemant;
    public Transform playerHand;
    public Animator playerAnimator;
    public Camera cameraFPS;
    public Camera cameraTPS;
    public Camera mainViewCamera;
    public CameraCTR cammeraControler;
    public SwitchScreenOnPress switchScreens;

    [Header("UI elements")]
    public Transform Ui;
    public RawImage bigScreenFPS;// first person view
    public RawImage smallScreenFPS;// minmap 
    public RawImage bigScreenTPS;// thirdPersonView
    public RawImage smallScreenTPS; // minimap
    public Slider strengthSlider; //manages value

    public Slider dangerSlider; // manages critical state of item


}
