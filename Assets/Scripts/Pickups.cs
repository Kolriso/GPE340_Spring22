using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickups : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onSpawn;
    public UnityEvent onPickup;
    // Start is called before the first frame update
    public virtual void Start()
    {
        onSpawn.Invoke();
    }

    // Update is called once per frame
    public abstract void Update();   
    
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
