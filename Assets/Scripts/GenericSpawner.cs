using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject objectToSpawn;
    public Mesh spawnMesh;
    [Header("Data")]
    public float respawnTime;

    private GameObject spawnedObject;
    private float countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        // If the spawn object is already spawned
        if (spawnedObject != null)
        {
            // Do nothing
            return;
        }
        else
        {
            // Countdown our timer
            countDown -= Time.deltaTime;

            // If our countdown hits zero
            if (countDown <= 0)
            {
                // Spawn (and store) the object
                spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation) as GameObject;
                // Reset the countdown
                countDown = respawnTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 myNewCoordinateSystem = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(Color.red, Color.clear, 0.7f);
        Gizmos.matrix = myNewCoordinateSystem;
        Gizmos.DrawMesh(spawnMesh, new Vector3(0, 0, 0), Quaternion.identity);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward);
    }
}
