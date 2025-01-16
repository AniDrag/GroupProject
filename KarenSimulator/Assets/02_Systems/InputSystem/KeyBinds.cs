using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Keycodes", menuName = "Tools/KeyCode Set")]

public class KeyBinds : ScriptableObject
{
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
}