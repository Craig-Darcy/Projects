using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class basicmovement1 : MonoBehaviour
{

    public float xcoord = 0.0f;  //declaring float variable xcoord
    public float ycoord = 0.0f;  //declaring float variable ycoord
    public float zcoord = 0.0f;  //declaring float variable zcoord

    public CharacterController playerController; //Creates a variable of type CharacterController called EthanController
    public Vector3 moveDirection = Vector3.zero; //New Vector3 variable called moveDirection set to 0,0,0
    public float gravity = 10.0f; //New float variable gravity set to 100.0f

    public bool canMove; //Boolean for the that allows you to lock the movement of the player for cutscenes,dialogue,etc
    public float moveSpeed; //The speed the player moves around the scene

    public bool playerIsMoving = true;

    
    
    void Start()
    {
        playerController = GetComponent<CharacterController>(); //CharacterController in scene is linked to variable EthanController
        AllowMove();
        
    }


    void Update()
    {

        if (playerController.isGrounded == true && canMove == true) //Sets an if statement that is only valid when Ethan(The Controller) is ground level
        {
            moveDirection.Set(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));     //Moves the CharacterController and Character. This line sets moveDirection variable to take input from the up/down cursor keys
            moveDirection = transform.TransformDirection(moveDirection);  //This forces the character forward facing to be the same as moveDirection
        }

        if(moveDirection.x == 0 && moveDirection.z == 0)
        {
            playerIsMoving = false;
        }
        else
        {
            playerIsMoving = true;
        }
       

        //Calculates the direction appropriately for the y coord
        playerController.Move(moveDirection * moveSpeed);   //Alters the speed at which the character moves at
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        xcoord = transform.position.x;   //Sets the current x position
        ycoord = transform.position.y;   //Sets the current y position
        zcoord = transform.position.z;   //Sets the current z position
        

        if (Input.GetKeyDown(KeyCode.Space) == true && playerController.isGrounded == true)    //Players Jump
        {                                             
            moveDirection.y = 3.0f;
        }
        playerController.Move(moveDirection * moveSpeed * Time.deltaTime);
        if (canMove == false) //Stops the player for deaths
        {
            moveDirection = Vector3.zero;
            playerController.Move(moveDirection * 0.0f);
            
            
           


        }
    }

    // Made for use with fungus allowing us to stop the player for use with dialogue
    public void AllowMove()
    {
        canMove = true;
    }
    public void StopMove()
    {
        canMove = false;
    }
}