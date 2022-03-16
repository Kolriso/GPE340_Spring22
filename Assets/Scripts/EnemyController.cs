using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private float timeForNextNavigationCheck;
    [SerializeField] private float timeBetweenNavigationChecks = 0.5f;
    [Header("Target")]
    public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Set my timer
        timeForNextNavigationCheck = Time.time + timeBetweenNavigationChecks;
        // Load my components
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeForNextNavigationCheck)
        {
            // Do check
            agent.SetDestination(targetTransform.position);
            // Reset timer
            timeForNextNavigationCheck = Time.time + timeBetweenNavigationChecks;
        }

        // Get the target velocity of the NavMesh agent
        Vector3 desiredVelocity = agent.desiredVelocity;

        // Set from world direction to local direction
        desiredVelocity = transform.InverseTransformDirection(desiredVelocity);

        // TODO: Change this use the pawn's speed - so it is exactly like player
        float tempSpeedDeleteMe = 3;
        desiredVelocity = desiredVelocity.normalized * tempSpeedDeleteMe;

        // Pass this in the animator
        anim.SetFloat("Foward", desiredVelocity.z);
        anim.SetFloat("Right", desiredVelocity.x);

        // Rotate towards the player
        Quaternion rotationToMovementDirection = Quaternion.LookRotation(agent.desiredVelocity, Vector3.up);

        // TODO: Get rotation speed from pawn
        float maxRoatationSpeedTempDeleteMe = 360;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMovementDirection, maxRoatationSpeedTempDeleteMe * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        // This runs everytime the animator causes us to move in position
        // Tell the NavMesh Agent that we already moved (so it doesn't have to)
        agent.velocity = anim.velocity;
    }
}
