using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [Header("Ragdoll Toggle")]
    public bool isRagdoll;

    private Rigidbody mainRigidbody;
    private Collider mainCollider;
    private Animator animator;
    private List<Rigidbody> ragdollRigidbodies;
    private List<Collider> ragdollColliders;

    // Start is called before the first frame update
    void Start()
    {
        // Load the variables
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        ragdollRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());

        ragdollRigidbodies.Remove(mainRigidbody);
        ragdollColliders.Remove(mainCollider);

        if (isRagdoll)
        {
            ActivateRagdoll();
        }
        else
        {
            DeactivateRagdoll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TEST CODE  DELETE ME
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleRagdoll();
        }
    }

    public void ToggleRagdoll()
    {
        isRagdoll = !isRagdoll;
        if (isRagdoll)
        {
            ActivateRagdoll();
        }
        else
        {
            DeactivateRagdoll();
        }
    }

    public void ActivateRagdoll()
    {
        // Turn ON ALL of the ragdoll colliders
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }
        // Turn ON ALL of the ragdoll rigidbodies
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }

        // Turn OFF the main collider
        mainCollider.enabled = false;
        // Turn OFF the main rigidbody
        mainRigidbody.isKinematic = true;
        // Turn OFF the animator
        animator.enabled = false;

        ragdollRigidbodies[1].AddForce(-transform.forward * 5000f);
    }

    public void DeactivateRagdoll()
    {
        // Turn OFF ALL of the ragdoll colliders
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = false;
        }
        // Turn OFF ALL of the ragdoll rigidbodies
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }

        // Turn ON the main collider
        mainCollider.enabled = true;
        // Turn ON the main rigidbody
        mainRigidbody.isKinematic = false;
        // Turn ON the animator
        animator.enabled = true;
    }
}
