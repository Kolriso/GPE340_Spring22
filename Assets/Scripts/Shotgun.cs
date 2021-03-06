using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    [Header("Fire Point")]
    public Transform firePoint;

    [Header("Projectile")]
    public GameObject projectilePrefab;

    [Header("Data")]
    public float projectileMoveSpeed;
    public float projectileLifeSpan;
    public int pelletCount;
    public float spreadAngle;
    AudioSource shotgunSound;

    [Header("Lists")]
    List<Quaternion> pellets;

    public void Awake()
    {
        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shotgunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void ShotgunBlast()
    {
        shotgunSound.Play();
        int i = 0;
        foreach(Quaternion quat in pellets)
        {
            Quaternion randomRotation = Random.rotation;
            // Instantiate a bullet at the fire location of this rifle
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectile.transform.rotation = Quaternion.RotateTowards(projectile.transform.rotation, randomRotation, spreadAngle);
            projectile.layer = gameObject.layer;
            // Transfer important information (like damage done) to the bullet
            if (projectileScript != null)
            {
                projectileScript.damageDone = damageDone;
                projectileScript.moveSpeed = projectileMoveSpeed;
                projectileScript.lifeSpan = projectileLifeSpan;
            }
            i++;
        }

        

        // The projectile script will handle the rest
    }
}
