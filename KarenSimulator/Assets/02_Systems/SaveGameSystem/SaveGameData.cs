using UnityEngine;

[CreateAssetMenu(fileName = "SaveName", menuName = "Tools/SaveOBJ")]
public class SaveGameData : ScriptableObject
{
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
}
