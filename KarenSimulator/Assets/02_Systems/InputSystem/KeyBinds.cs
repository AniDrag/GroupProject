using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Keycodes", menuName = "Tools/KeyCode Set")]

public class KeyBinds : ScriptableObject
{
    /* public enum keycodeMechanic
     {
          None,
          Interact,
          Menu,
          UseItem,
          SprintHold,
          SprintToggle,
          WalkToggle,
          CrouchHold,
          CrouchToggle,
          Jump,
          Attack,
          Block,
          AimToggle,
          AimHold,
          Reload,
          CancleCast,
          Weapon1,
          Weapon2, 
          Weapon3,
     }*/

      [Header("Player Interactions")]
      public KeyCode interact = KeyCode.F;
      public KeyCode inventory = KeyCode.Tab;
      public KeyCode menu = KeyCode.Escape;
      public KeyCode useItem = KeyCode.E;
    public KeyCode activateMouse = KeyCode.Q;

      [Header("Player Movemant")]
      public KeyCode sprintHold = KeyCode.LeftShift;
      public KeyCode crouchToggle = KeyCode.C;
      public KeyCode jump = KeyCode.Space;

      [Header("Player Combat")]
      public KeyCode attack = KeyCode.Mouse0;
      public KeyCode cancleCast = KeyCode.R;
}