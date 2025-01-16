using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerBaseMovemant : MonoBehaviour
{
    [Header("Movemant Details")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    public float jumpHeight;
    [SerializeField] float gravityValue = 9.81f;

    [Header("Ground details")]
    [SerializeField] float verticalVelocity;
    [SerializeField] float groundedTimer;
    [SerializeField] float groundDrag;

    public enum PlayerStates
    {
        Idle,
        Walking,
        Runing,
        Crouching,
        Jumping,
        FreeFalling,
        RunAndJump,
        CrouchAndRun,
        RunAdnCrouch
    }
    [Header("Animation Controls")]
    public PlayerStates state;


    [Header("Refrences")]
    [SerializeField] SaveGameData saveGameData;
    [SerializeField] Transform GroundCheckHitbox;
    [SerializeField] Transform playerOrientation;
    [SerializeField] Transform thirdPersonTransform;
    [SerializeReference] Animator playerAnimations;
    CharacterController playerBody;


    [Header("Debug View")]
    float currentSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool jumped;
    [SerializeField] bool groundedPlayer;
    public bool fps;
    private void Awake()
    {
        playerBody = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Grounded();
        PlayerInput();
        if (!fps)
        {
            RotatePov();
        }

    }
    void PlayerInput()
    {
        // gather lateral input control
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Align movement direction to player orientation
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        // scale by speed
        moveDirection *= currentSpeed;

        //run and walk state
        if (Input.GetKey(saveGameData.playerPrefs.sprintHold) && !jumped)
        {
            Debug.Log("sprinting");
            playerAnimations.SetBool("Running", true);
            state = PlayerStates.Runing;
            currentSpeed = runSpeed;
            if (playerAnimations.GetBool("Walking") == true)
            {
                playerAnimations.SetBool("Walking", false);
            }
        }
        else if (!jumped)
        {
            if(playerAnimations.GetBool("Running") == true && !Input.GetKey(saveGameData.playerPrefs.sprintHold))
            {
                playerAnimations.SetBool("Running", false);
            }
            playerAnimations.SetBool("Walking", true);
            state = PlayerStates.Walking;
            currentSpeed = walkSpeed;
        }
        if (moveDirection == Vector3.zero && !jumped)
        {
            playerAnimations.SetBool("Walking", false);
            playerAnimations.SetBool("Running", false);
            playerAnimations.SetBool("Jump", false);
            state = PlayerStates.Idle;
        }

        // allow jump as long as the player is on the ground
        if (Input.GetKeyDown(saveGameData.playerPrefs.jump))
        {
            if (groundedTimer > 0)
            {
                groundedTimer = 0;
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
                jumped = true;
                state = PlayerStates.Jumping;
                playerAnimations.SetBool("Jump", true);
            }
        }

        // inject Y velocity before we use it
        moveDirection.y = verticalVelocity;

        // call .Move() once only
        playerBody.Move(moveDirection * Time.deltaTime);
    }
    // ground check if player is in contact with the ground
    void Grounded()
    {

        groundedPlayer = playerBody.isGrounded;
        if(!groundedPlayer && jumped)
        {
            Invoke("FreeFall", 1);
        }
        if(jumped && groundedPlayer)
        {
            jumped = false;
            playerAnimations.SetBool("Jump", false);
        }
        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        // slam into the ground
        if (groundedPlayer && verticalVelocity < 0)
        {
            // hit ground
            verticalVelocity = 0f;
        }

        // apply gravity always, to let us track down ramps properly
        verticalVelocity -= gravityValue * Time.deltaTime;
    }

    void FreeFall()
    {
        state = PlayerStates.FreeFalling;
        
    }
    public void ResetOrientation()
    {
        playerOrientation.rotation = Quaternion.Euler(0,0,0);
    }
    void RotatePov()
    {
        if (moveDirection != Vector3.zero) // Ensure there's movement
        {
            // Calculate the target rotation angle in the Y-axis to face the movement direction
            float targetYRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

            // Create a rotation quaternion with the target Y rotation (keep X and Z rotations as 0)
            Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

            // Smoothly rotate the thirdPersonTransform (camera) towards the target direction
            thirdPersonTransform.rotation = Quaternion.Slerp(thirdPersonTransform.rotation, targetRotation, Time.deltaTime * 10f); // Adjust 10f for rotation speed
        }
        


    }
}
