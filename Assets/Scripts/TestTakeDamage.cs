using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTakeDamage : MonoBehaviour
{
    float damage = 10.0f;

    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();
        
        if (otherHealth != null)
        {
            otherHealth.takeDamage(damage);
        }
    }
}
