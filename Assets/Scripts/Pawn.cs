using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private Animator anim; // Creating an animator variable
    public float moveSpeed = 1; // Meters per second
    public float rotateSpeed = 360; // Degrees per second

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 moveVector)
    {
        // Apply speed
        moveVector = moveVector * moveSpeed;

        // Set the animation to move on the x axis
        anim.SetFloat("Right", moveVector.x);
        // Set the animation to move on the z axis
        anim.SetFloat("Forward", moveVector.z);
    }

    public void RotateTowards(Vector3 targetPoint)
    {
        // Find the roation that would be looking at that target point
        // Find the vector to the point
        Vector3 targetVector = targetPoint - transform.position;

        // Find roatation down that vector
        Quaternion targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);

        // Change my roation (slowly) towards that targeted roation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
