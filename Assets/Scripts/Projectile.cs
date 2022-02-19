using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Values")]
    public float damageDone;
    public float moveSpeed;
    public float lifeSpan;

    [Header("Rigidbody")]
    public Rigidbody rb;

    private void Awake()
    {
        // Load the rigid body component
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward
        rb.MovePosition(transform.position + (transform.forward * moveSpeed * Time.deltaTime));
    }

    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();

        if (otherHealth != null)
        {
            otherHealth.takeDamage(damageDone);
        }

        Destroy(gameObject);
    }
}
