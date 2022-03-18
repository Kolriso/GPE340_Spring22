using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onDie;

    [Header("Values")]
    [SerializeField] private float maxHealth = 100.0f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float amountOfDamage)
    {
        // Call the onDamage event
        onDamage.Invoke();

        // Subtract the health here when damage is applied
        currentHealth -= amountOfDamage;

        // If our health <= 0, the player dies
        if (currentHealth <= 0)
        {
            // Call the onDie event
            onDie.Invoke();

            Destroy(gameObject);
        }
        else
        {
            // This here doesn't allow the health to go over max health
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }
}