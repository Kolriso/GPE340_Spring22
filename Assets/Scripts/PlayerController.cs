using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Class Variables"), Tooltip("How to access things from other classes")]
    public Pawn pawn; // Created a Pawn variable to get access to the pawn
    public Camera playerCamera; // A variable to hold the player's camera

    [Header("Speed Variables"), Tooltip("The variables that hold the sprint and walk values")]
    [SerializeField] private float sprint = 2.0f; // The variable for the sprint value
    [SerializeField] private float walk = 0.5f; // The variable for the walk value

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

        // Adjust this value, so that it's based on the direction the character's facing
        moveVector = pawn.transform.InverseTransformDirection(moveVector);

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

        // Read fire button inputs
        GetButtonInputs();

        // Rotate player to face the mouse
        RotateToMouse();
    }

    private void GetButtonInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            pawn.weapon.onStartAttack.Invoke();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            pawn.weapon.onEndAttack.Invoke();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            pawn.weapon.onAlternateAttackStart.Invoke();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            pawn.weapon.onAlternateAttackEnd.Invoke();
        }
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
