using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickups
{
    [Header("Weapon")]
    public GameObject weaponToPickup;

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
        Pawn otherPawn = other.GetComponent<Pawn>();

        if (otherPawn != null)
        {
            otherPawn.EquipWeapon(weaponToPickup);
        }


        if (other.gameObject.layer == 6)
            GameManager.instance.uiManager.weaponSprite.sprite = weaponToPickup.GetComponent<Weapons>().weaponSprite;

        base.OnTriggerEnter(other);
    }
}
