using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M16 : Weapons
{
    [Header("Fire Point")]
    public Transform firePoint;

    [Header("Projectile")]
    public GameObject projectilePrefab;

    [Header("Data")]
    public float projectileMoveSpeed;
    public float projectileLifeSpan;
    AudioSource machineGunSound;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run the start function from the parent class
        base.Start();
        machineGunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Run the update function from the parent class
        base.Update();
    }

    public void ShootBullet()
    {
        machineGunSound.Play();
        // Instantiate a bullet at the fire location of this rifle
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectile.layer = gameObject.layer;
        // Transfer important information (like damage done) to the bullet
        if (projectileScript != null)
        {
            projectileScript.damageDone = damageDone;
            projectileScript.moveSpeed = projectileMoveSpeed;
            projectileScript.lifeSpan = projectileLifeSpan;
        }

        // The projectile script will handle the rest
    }

    public void ShootAutomatic()
    {
        machineGunSound.Play();
        // Instantiate a bullet at the fire location of this rifle
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectile.layer = gameObject.layer;
        // Transfer important information (like damage done) to the bullet
        if (projectileScript != null)
        {
            isAutoFiring = true;
            fireDelay = 0.3f;
            projectileScript.damageDone = damageDone;
            projectileScript.moveSpeed = projectileMoveSpeed;
            projectileScript.lifeSpan = projectileLifeSpan;
        }
    }
}
