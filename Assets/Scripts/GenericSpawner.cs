using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
    [Header("Game Objects")]
    public RandomWeightedObjects[] itemDrops;
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
                spawnedObject = Instantiate(ChooseSpawnObject(), transform.position, transform.rotation) as GameObject;
                // Reset the countdown
                countDown = respawnTime;
            }
        }
    }

    public GameObject ChooseSpawnObject()
    {
        // Variable to hold spawn object
        GameObject objectToSpawn;

        // Create a second parallel array - this holds the cutoff (where it changes to the next type)
        float[] CDFArray = new float[itemDrops.Length];
        
        // Variable to hold cumulative density (total weights so far)
        float cumulativeDensity = 0;
        for (int i = 0; i < itemDrops.Length; i++)
        {
            // Add this object's weight, so we know where the cutoff is
            cumulativeDensity += itemDrops[i].weight;
            // Store that in the CDF Array
            CDFArray[i] = cumulativeDensity;
        }

        // Choose a random number up to the max cutoff
        float rand = Random.Range(0.0f, cumulativeDensity);

        /***Old one at a time method--it's slower, but works
        // Look through my CDF to find where our random number would fall - which CDF index it fall under
        for (int i = 0; i < CDFArray.Length; i++)
        {
            if (rand < CDFArray[i])
            {
                objectToSpawn = objectsToSpawn[i].objectToSpawn;
                return objectToSpawn;
            }
        }
        ***/

        int selectedIndex = System.Array.BinarySearch(CDFArray, rand);

        // If selected index is negative...
        if (selectedIndex < 0)
        {
            // It's not the exact value, we have to FLIP (bitwise not) the value to find the index we want
            selectedIndex = ~selectedIndex;
        }

        objectToSpawn = itemDrops[selectedIndex].objectToSpawn;
        return objectToSpawn;
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
