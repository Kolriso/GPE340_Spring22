using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Events")]
    [SerializeField, Tooltip("Used every time the object is healed.")]
    private UnityEvent onHeal;
    [SerializeField, Tooltip("Used every time the object is takes damage.")]
    private UnityEvent onDamage;
    [SerializeField, Tooltip("Used only once and that is when health reaches 0.")]
    private UnityEvent onDie;

    [Header("Values")]
    [SerializeField] private float maxHealth = 1.0f;
    public float currentHealth;

    [Header("UI")]
    public Image healthBarImage;
    public Pawn player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10.0f);
        }
    }

    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1.0f);
    }

    public void takeDamage(float amountOfDamage)
    {
        // Call the onDamage event
        onDamage.Invoke();

        // Subtract the health here when damage is applied
        currentHealth -= amountOfDamage;

        UpdateHealthBar();

        // If our health <= 0, the player dies
        if (currentHealth <= 0)
        {
            // Call the onDie event
            onDie.Invoke();
        }
        else
        {
            // This here doesn't allow the health to go over max health
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    public void Healing(float amountToHeal)
    {
        // Call the onHeal event
        onHeal.Invoke();

        // Add to the current health when there is an amount to heal
        currentHealth += amountToHeal;

        // Make sure you don't go over the max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        UpdateHealthBar();
    }
}
