using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickups
{
    Health playerHealth;
    private float healthBonus = 10.0f;

    private void Awake()
    {
        playerHealth = FindObjectOfType<Health>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        playerHealth.Healing(healthBonus);
    }
}
