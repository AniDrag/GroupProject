using UnityEngine;

[CreateAssetMenu(fileName = "SaveName", menuName = "Tools/SaveOBJ")]
public class SaveGameData : ScriptableObject
{
    [Header("Player Details")]
    [Tooltip("Name of the player for this save file.")]
    public string playerName;

    [Header("Progress Tracking")]
    [Tooltip("Player's current score.")]
    public int score;
    public int multiplier = 2;
    public float timescore;

    // Enum for camera movement types
    public enum CameraMovemantType
    {
        Default,
        InvertedAll,       // Both axes are inverted
        InvertedHorizontal, // Only horizontal axis is inverted
        InvertedVertical  // Only vertical axis is inverted
                // No inversion
    }

    [Header("Settings")]
    [Tooltip("Horizontal sensitivity multiplier for camera movement.")]
    [Range(1f, 2f)]
    public float horizontalSensitivity = 1f;

    [Tooltip("Vertical sensitivity multiplier for camera movement.")]
    [Range(1f, 2f)]
    public float verticalSensitivity = 1f;

    [Tooltip("Type of camera movement inversion.")]
    public CameraMovemantType camMoveTypes = CameraMovemantType.Default;

    [Tooltip("Field of view for the player camera.")]
    [Range(60f, 110f)]
    public float fieldOfView = 60f;

    [Tooltip("Master volume for game audio.")]
    [Range(0f, 100f)]
    public float masterVolume = 100f;

    [Header("Key Bindings")]
    [Tooltip("Player's custom key bindings.")]
    public KeyBinds inputKeys;

    [Header("Save Details")]
    [Tooltip("The current scene index for this save.")]
    public int currentScene;

    [Tooltip("Unique ID for this save file.")]
    public int gameSaveID;


    /*
    [Header("Player Details")]
    public string playerName;

    [Header("Progress tracking")]
    public int score;

    public enum CameraMovemantType{
        InvertedAll,
        InvertedHorizontal,
        InvertedVertical,
        Default
    }
    [Header("Settings")]
    [Range(1,2)] public float horizontalSensitivity;
    [Range(1, 2)] public float verticalSensitivity;
    public CameraMovemantType camMoveTypes;
    [Range(60, 110)] public float fieldOfView;
    [Range(0, 100)] public float masterVolume;

    [Header("Keys")]
    public KeyBinds playerPrefs;

    [Header("Save Details")]
    public int currentScene;
    public int gameSaveID;
    */
}
