using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouse : MonoBehaviour
{
    public Transform Player; //Player Variable
    public Camera MainCamera; //Camera Variable
    float mouseY; //Input for mouses Y axis which allows it to be restricted
    public bool mouseMovement;
    //Mouse Settings Variables
        //Prevents camera from over rotating on the player
    public float minY = -90.0f; //Minimum value for the Y rotation
    public float maxY = 90.0f; //Maximum value for the Y rotation

        //Mouse Sensitivity
    public float sensX = 5.0f; //Sensitivity for X axis of the mouse
    public float sensY = 5.0f; //Sensitivity for Y axis of the mouse

    float rotationX = 0.0f; //Float variable rotationX;
    float rotationY = 0.0f; //Float variable rotationY;

    //Variable to move the players body
    Vector3 rotatePlayer;
    void Start()
    {
        AllowMouseMove();
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        RotateCamera();
    }

    void RotateCamera()
    {
        rotationX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime ; //Controls the mouse rotation on the X axis
        mouseY = Input.GetAxis("Mouse Y") * sensY* Time.deltaTime; //Controls the mouse rotation on the Y axis
        rotationY -= mouseY;

        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(rotationY, 0, 0); //Controls the degree angles

        Player.Rotate(Vector3.up * rotationX);

        if (mouseMovement == false)
        {
            rotationX = Input.GetAxis("Mouse X") * 0;
            mouseY = Input.GetAxis("Mouse Y") * 0;
        }
    }

    public void AllowMouseMove()
    {
        mouseMovement = true;
    }
    public void StopMouseMove()
    {
        mouseMovement = false;
    }

}