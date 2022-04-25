using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapons : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onShoot;
    public UnityEvent onStartAttack;
    public UnityEvent onEndAttack;
    public UnityEvent onAlternateAttackStart;
    public UnityEvent onAlternateAttackEnd;
    public UnityEvent onReload;

    [Header("States")]
    public bool isAutoFiring;
    [Header("Data")]
    public float fireDelay; // Seconds between shots
    public float countDown;
    public float damageDone;
    public Sprite weaponSprite;
    [Header("Hand Position")]
    public Transform RHPoint;
    public Transform LHPoint;

    // Start is called before the first frame update
   public virtual void Start()
    {
        countDown = fireDelay;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Subtract the time it took to play the last frame from our countdown
        countDown -= Time.deltaTime;
        
        if (isAutoFiring)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (countDown <= 0)
        {
            // Shoot
            onShoot.Invoke();
            // Reset the timer
            countDown = fireDelay;
        }
    }

    public void StartAutoFire()
    {
        isAutoFiring = true;
    }

    public void EndAutoFire()
    {
        isAutoFiring = false;
    }

    public void ToggleAutoFire()
    {
        isAutoFiring = !isAutoFiring;
    }
}
