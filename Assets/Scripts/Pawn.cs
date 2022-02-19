using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("Animator"), Tooltip("How to get access to the animator within the code")]
    private Animator anim; // Creating an animator variable
    [Header("Movement Values"), Tooltip("The variables that hold the initial speed and the rotation speed")]
    public float moveSpeed = 1; // Meters per second
    public float rotateSpeed = 540; // Degrees per second
    [Header("Weapon")]
    public Weapons weapon;
    public Transform weaponMountPoint;
    [Header("IK Controller")]
    public Transform rightHandPoint;
    public Transform leftHandPoint;
    public float rightHandWeight;
    public float leftHandWeight;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnequipWeapon()
    {
        // Destroy the equiped weapon
        Destroy(weapon.gameObject);

        // Make sure the weapon variables is set to null
        weapon = null;
    }

    public void EquipWeapon(GameObject weaponPrefabToEquip)
    {
        // Unequip the old weapon
        UnequipWeapon();

        // Instantiate the weapon to equip
        GameObject newWeapon = Instantiate(weaponPrefabToEquip, weaponMountPoint.position, weaponMountPoint.rotation);
        
        // Make it so the weapon's parent (transform.parent) is the correct part of the player
        newWeapon.transform.parent = weaponMountPoint;
        
        // Set this pawn so the new weapon is the weapon we use
        weapon = newWeapon.GetComponent<Weapons>();
    }

    /// <summary>
    /// This is the function to get the animations hooked up for the movement of the pawn
    /// You take the moveVector parameter & multiply it by the move speed then set what axis the pawn will move on
    /// </summary>
    public void Move(Vector3 moveVector)
    {
        // Apply speed
        moveVector = moveVector * moveSpeed;

        // Set the animation to move on the x axis
        anim.SetFloat("Right", moveVector.x);
        // Set the animation to move on the z axis
        anim.SetFloat("Forward", moveVector.z);
    }
    /// <summary>
    /// This is the function to get the pawn to rotate on the z axis
    /// It is taking the vector3 variable and a quaternion variable in order to do this 
    /// </summary>
    public void RotateTowards(Vector3 targetPoint)
    {
        // Find the roation that would be looking at that target point
        // Find the vector to the point
        Vector3 targetVector = targetPoint - transform.position;

        // Find roatation down that vector
        Quaternion targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);

        // Change my rotation (slowly) towards that targeted roation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPoint.position);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPoint.position);

        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandPoint.rotation);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandPoint.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
    }
}
