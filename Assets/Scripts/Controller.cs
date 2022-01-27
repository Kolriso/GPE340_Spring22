using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn pawn; // Created a Pawn variable to get access to the pawn
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
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

        // Rotate player to face the mouse
        RotateToMouse();
    }

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
