using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    // Transform variables for the player
    public Transform player;
    public Transform playerCamera;

    //Object variables
    public float forceOfThrow = 10;
    public bool playerPresent = false;
    public bool beingCarried = false;
    public bool touched = false;



    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position); //Calculates the distance between the player and the object

        //Checks the players distance and allows object pickup
        if (dist <= 2.5f)
        {
            playerPresent = true;
        }
        else
        {
            playerPresent = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && playerPresent == true) //Check if the interaction key is pressed and object is allowed to be picked up
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCamera;
            beingCarried = true;
        }

        if (beingCarried == true) //Checks if the object is being carried
        {
            if (touched == true) //Drops object if it touches a wall/other object (prevents clipping)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }

            if (Input.GetMouseButtonDown(0)) //Throws object when left mouse button is pressed
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCamera.forward * forceOfThrow);
            }
            else if (Input.GetMouseButtonDown(1)) //Drops the object when right mouse button is pressed
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }

    void OnTriggerEnter() //Sets touched to true if it collides with another object
    {
        if (beingCarried == true)
        {
            touched = true;
        }
    }
}
