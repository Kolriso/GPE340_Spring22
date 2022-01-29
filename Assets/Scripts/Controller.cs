using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn pawn; // Created a Pawn variable to get access to the pawn
    public Camera playerCamera; // A variable to hold the player's camera
    private float sprint = 2.0f; // A variable to to calculate sprint speed
    private float walk = 0.5f; // A varaible to calculate the walk speed

    // Start is called before the first frame update
    void Start()
    {
        // An if statement to check whether or not a camera is hooked up
        if (playerCamera == null) Debug.LogWarning("Error: No camera set!");
    }

    // Update is called once per frame
    void Update()
    {
        // Send my move command to my pawn
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Limit the vector magnitude to 1
        moveVector = Vector3.ClampMagnitude(moveVector, 1.0f);
        // Tell the pawn to move
        pawn.Move(moveVector);

        // An if statement to see whether or not the left shift or Z button is being pressed or not in order to walk, run, or sprint
        if (Input.GetKey(KeyCode.Z))
        {
            pawn.moveSpeed = walk;
        } 
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            pawn.moveSpeed = sprint;
        }
        else
        {
            pawn.moveSpeed = 1.0f;
        }

        // Rotate player to face the mouse
        RotateToMouse();
    }

    // Allows the character to move in the direction of the mouse pointer
    void RotateToMouse()
    {
        // Create a plane object (a mathematical representation of all the point in 2D)
        Plane groundPlane;

        // Set that plane so it is the X,Z plane the player is standing on
        groundPlane = new Plane(Vector3.up, pawn.transform.position);

        // Cast a ray from our camera toward the plane, through our mouse cursor
        float distance; // Variable to hold the distance from the ray to the ground plane
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition); // Creating a camera ray from the mouse position
        groundPlane.Raycast(cameraRay, out distance); // Figuring out the distance from the camera ray to the gorund plane

        // Find where that ray hits the plane
        Vector3 raycastPoint = cameraRay.GetPoint(distance);

        // Rotate towards that point 
        pawn.RotateTowards(raycastPoint);
    }
}
