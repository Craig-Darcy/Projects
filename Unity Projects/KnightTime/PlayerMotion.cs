using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMotion : MonoBehaviour
{
    #region Variables

    PlayerManager playerManager; // Script to manage all the scripts that interact with the player
    PlayerInput playerInputManager; // Script to manage the inputs of the player
    PlayerAnimator playerAnimator; // Script to manage the animations of the player character
    public PlayerCamera playerCamera; // Camera that is attached to the player (Cinemachine does most of the work here)
    [HideInInspector]
    public Vector3 moveDirection; // Allows the move direction of the player to be interacted with by other scripts hidden from inspector as it is not needed to be seen

    public Transform mainCamera; // Transform data for the camera to allow the player to control it
    public Rigidbody playerRB; // Calls rigidbody attached to Player character

    //Bools for the state of the player
    public bool isGrounded;
    public bool isJumping;

    //The speed for how far down the analog stick is pressed (Mouse and Keyboard is always running will be looked at in future)
    public float runningSpeed = 7;
    public float moveSpeed = 5;
    public float slowSpeed = 3.5f;

    public float rotateSpeed = 15; //Speed the player character rotates

    public float playerHeight; // Height of the player hitbox for use with stairs
    public float airTime; // How long the player is in the air (Used for damage/death)
    public float leapingVelo; // Variable to add force so players don't get stuck on ledges
    public LayerMask groundLayer; //Layer mask to check if the player is grounded
    public float groundDrag; //Lower to make the ground for slippy heighten to make it more rough

    public float jumpHeight = 3; //How high the player jumps
    public float airSpeed = 5; //Speed the player travels when jumping
    public float gravityIntensity = -9; //How fast we want the player pulled back to the ground

    #endregion




    private void Awake()
    {
        isGrounded = true; //Set the player to be grounded by default
        
        //Gets the component needed for the variable to work
        playerManager = GetComponent<PlayerManager>();
        playerInputManager = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerRB = GetComponent<Rigidbody>();

        mainCamera = Camera.main.transform; //Sets the mainCamera to what is attached to the player when game is started this allows for camera changes
    }
    private void Start()
    {
        playerRB.freezeRotation = true; //Allows for smoother rotation
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer); //If the raycast check finds the ground layer returns true

        if (isGrounded)
        {
            playerRB.drag = groundDrag; //Prevents the player character from looking like he is always on ice
        }
        else
        {
            playerRB.drag = 0;
        }
        SpeedController();
    }
    public void AllMovementHandler()
    {
        HandleFalling();

        if (playerManager.isFalling)
        {
            return;
        }

        if (isJumping)
        {
            return;
        }


        HandleMove();
    }
    private void HandleMove()
    {
        if (playerManager.isFalling)
        {
            return;
        }

        moveDirection = playerCamera.orientation.forward * playerInputManager.inputVertical + playerCamera.orientation.right * playerInputManager.inputHorizontal; // Sets the movement direction to be based off the camera position
        if (isGrounded)
        {
            //How far the joy stick is pushed sets the speed for the player movement allowing for better control 
            if (playerInputManager.moveAmount == 1)
            {
                playerRB.AddForce(moveDirection.normalized * runningSpeed * 10f, ForceMode.Force);
            }
            else if (playerInputManager.moveAmount < 0.5f)
            {
                playerRB.AddForce(moveDirection.normalized * slowSpeed * 10f, ForceMode.Force);
            }
            else
            {
                playerRB.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
        }
        else if (!isGrounded)
        {
            //Same concept as above but for in the air instead
            if (playerInputManager.moveAmount == 1)
            {
                playerRB.AddForce(moveDirection.normalized * runningSpeed * 10f * airSpeed, ForceMode.Force);
            }
            else if (playerInputManager.moveAmount < 0.5f)
            {
                playerRB.AddForce(moveDirection.normalized * slowSpeed * 10f * airSpeed, ForceMode.Force);
            }
            else
            {
                playerRB.AddForce(moveDirection.normalized * moveSpeed * 10f * airSpeed, ForceMode.Force);
            }
        }
    }

    void SpeedController() //  The physics behind the movement of the player
    {
        Vector3 flatVelo = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

        if(flatVelo.magnitude > runningSpeed)
        {
            Vector3 limitedVelo = flatVelo.normalized * runningSpeed;
            playerRB.velocity = new Vector3(limitedVelo.x, playerRB.velocity.y, limitedVelo.z);
        }
    }

    private void HandleFalling() //Plays the animations for the player falling/landing and stops the players from getting stuck on ledges
    {

        if (!isGrounded && !isJumping) //Plays the falling animation and prevents getting stuck on ledges
        {
            if (!playerManager.isFalling)
            {
                playerAnimator.PlayAnimTarget("Falling_Idle", true);
            }
            airTime = airTime + Time.deltaTime;
            playerRB.AddForce(this.transform.forward * leapingVelo); //Prevents gettings stuck on ledges
            
        }

        if(Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer)) // Plays the landing animation when isGrounded is true
        {
            if (!isGrounded && playerManager.isFalling)
            {
                playerAnimator.PlayAnimTarget("Landing", true);
            }

            airTime = 0;
            isGrounded = true;
            playerManager.isFalling = false;
        }
        else
        {
            isGrounded = false;
        }
    }

   public void HandleJumping() //Physics behind the jumping in the game
    {
        
        if (isGrounded)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z); //Matches the velocity of where the player is currently going
            playerAnimator.anim.SetBool("isJumping", true); //Sets the bool in the animator to allow the animation to play through the override
            playerAnimator.PlayAnimTarget("Jumping", false); //Plays the jumping animation
            playerRB.AddForce(transform.up * jumpHeight, ForceMode.Impulse); //Adds the jump force to the rigid body moving the player up

        }
    }

    
}
