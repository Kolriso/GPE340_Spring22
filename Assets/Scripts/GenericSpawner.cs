using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject objectToSpawn;
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
                spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
                // Reset the countdown
                countDown = respawnTime;
            }
        }
    }
}
