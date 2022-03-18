using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Pawn pawn;
    private NavMeshAgent agent;
    private float timeForNextNavigationCheck;
    [SerializeField] private float timeBetweenNavigationChecks = 0.5f;
    [Header("Target")]
    public PlayerController player;

    public void Awake()
    {
        // Set my timer
        timeForNextNavigationCheck = Time.time + timeBetweenNavigationChecks;
        // Load my components
        agent = GetComponent<NavMeshAgent>();
        pawn = GetComponent<Pawn>();
    }
    // Start is called before the first frame update
    void Start()
    {
       if (player == null)
        {
            FindPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }
        else
        {
            MoveToPlayer();
        }
    }

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void MoveToPlayer()
    {
        if (Time.time >= timeForNextNavigationCheck)
        {
            // Do check
            agent.SetDestination(player.pawn.transform.position);
            // Reset timer
            timeForNextNavigationCheck = Time.time + timeBetweenNavigationChecks;
        }

        // Get the target velocity of the NavMesh agent
        Vector3 desiredVelocity = agent.desiredVelocity;

        // Set from world direction to local direction
        desiredVelocity = transform.InverseTransformDirection(desiredVelocity);

        // Adjust it for our pawn's speed - so it is exactly like player
        desiredVelocity = desiredVelocity.normalized * pawn.moveSpeed;

        // Pass this in the animator
        pawn.anim.SetFloat("Forward", desiredVelocity.z);
        pawn.anim.SetFloat("Right", desiredVelocity.x);

        // Rotate towards the player
        Quaternion rotationToMovementDirection = Quaternion.LookRotation(agent.desiredVelocity, Vector3.up);

        // Get rotation speed from pawn
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMovementDirection, pawn.rotateSpeed * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        // This runs everytime the animator causes us to move in position
        // Tell the NavMesh Agent that we already moved (so it doesn't have to)
        agent.velocity = pawn.anim.velocity;
    }
}
