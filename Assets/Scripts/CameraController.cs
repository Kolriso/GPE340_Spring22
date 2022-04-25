using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[Header("Class Variable"), Tooltip("How to access things from another class")]
    //public Pawn player; // A variable to store a reference to the player game object
    [Header("Vector3 Variable"), Tooltip("How to keep track of the distance between player and camera")]
    public Vector3 offset; // A variable to store the offset distance between the player and camera

    // Start is called before the first frame update
    void Start()
    {
        // Hook up the GameObject to the pawn
        //if (GameManager.instance.player.pawn == null) Debug.LogWarning ("Character not connected"); 

       // Calculate and store the offset value by getting the distance between the player's position and camera's position.
       //offset = transform.position - GameManager.instance.player.pawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player.pawn != null)
        {
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = GameManager.instance.player.pawn.transform.position + offset;
        }
        else
        {
            return;
        }
    }
}
