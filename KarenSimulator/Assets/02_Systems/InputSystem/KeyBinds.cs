using UnityEngine;


[CreateAssetMenu(fileName = "Keycodes", menuName = "Tools/KeyCode Set")]

public class KeyBinds : ScriptableObject
{
    [Header("Player Interactions")]
    public KeyCode interact = KeyCode.F;
    public KeyCode screenSwitch = KeyCode.Tab;
    public KeyCode menu = KeyCode.Escape;

    [Header("Player Movement")]
    public KeyCode sprintHold = KeyCode.LeftShift;
    public KeyCode crouchToggle = KeyCode.C;
    public KeyCode jump = KeyCode.Space;

    [Header("Player Combat")]
    public KeyCode attack = KeyCode.Mouse0;
    public KeyCode cancelCast = KeyCode.R;

    /// <summary>
    /// Validates that no key is duplicated. Helps prevent conflicts in key bindings.
    /// </summary>
    public void ValidateKeyBinds()
    {
        KeyCode[] allKeys = {
            interact, screenSwitch, menu,
            sprintHold, crouchToggle, jump,
            attack, cancelCast
        };

        for (int i = 0; i < allKeys.Length; i++)
        {
            for (int j = i + 1; j < allKeys.Length; j++)
            {
                if (allKeys[i] == allKeys[j])
                {
                    Debug.LogWarning($"Key bind conflict detected: '{allKeys[i]}' is assigned to multiple actions.");
                }
            }
        }
    }

    /// <summary>
    /// Resets all key bindings to their default values.
    /// </summary>
    public void ResetKeyBinds()
    {
        interact = KeyCode.F;
        screenSwitch = KeyCode.Tab;
        menu = KeyCode.Escape;

        sprintHold = KeyCode.LeftShift;
        crouchToggle = KeyCode.C;
        jump = KeyCode.Space;

        attack = KeyCode.Mouse0;
        cancelCast = KeyCode.R;

        Debug.Log("Key bindings reset to default values.");
    }


    /*
      [Header("Player Interactions")]
      public KeyCode interact = KeyCode.F;
      public KeyCode screenSwitch = KeyCode.Tab;
      public KeyCode menu = KeyCode.Escape;

      [Header("Player Movemant")]
      public KeyCode sprintHold = KeyCode.LeftShift;
      public KeyCode crouchToggle = KeyCode.C;
      public KeyCode jump = KeyCode.Space;

      [Header("Player Combat")]
      public KeyCode attack = KeyCode.Mouse0;
      public KeyCode cancleCast = KeyCode.R;
    */
}