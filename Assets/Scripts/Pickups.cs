using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickups : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onSpawn;
    public UnityEvent onPickup;

    [Header("Data")]
    public float rotationSpeed = 100.0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        onSpawn.Invoke();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Rotate the pickup 
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
    
    public virtual void OnTriggerEnter(Collider other)
    {
        Pawn otherPawn = other.GetComponent<Pawn>();

        if (otherPawn != null)
        {
            onPickup.Invoke();
            Destroy(gameObject);
        }
    }
}
