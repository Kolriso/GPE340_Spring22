using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAlien : AIController
{
    private Pawn pawn;
    private NavMeshAgent agent;
    private float timeForNextNavigationCheck;
    
    private Vector3 leadVector; // How far in front of (or otherwise away from) player to shoot
    private float noLeadDistance = 0.0f; // Use 0% of lead modifier
    private float maxLeadDistance = 15.0f; // Use 100% of lead modifier

    [Header("Lead Modifier")]
    public float leadModifier = 1.0f;
    
    [Header("Navigation Check")]
    [SerializeField] private float timeBetweenNavigationChecks = 0.5f;
    
    [Header("Shooting Error")]
    public float maxShootingError = 1.0f;

    [Header("Shooting Distance")]
    public float minShootDistance = 10.0f;
    public float maxShootDistance = 25.0f;

    [Header("Target")]
    public PlayerController player;

    // Awake is called when the object is created
    public override void Awake()
    {
        // Set my timer
        timeForNextNavigationCheck = Time.time + timeBetweenNavigationChecks;
        // Load my components
        agent = GetComponent<NavMeshAgent>();
        pawn = GetComponent<Pawn>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        if (player == null)
        {
            FindPlayer();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }
        else
        {
            SetLeadVector();
            MoveToPlayer();
            RotateTowardsPlayer();
            
            if (pawn.weapon != null)
            {
                ShootAtPlayer();
            }
        }
    }

    public void SetLeadVector()
    {
        // Find the distance to the player
        float distanceToPlayer = Vector3.Distance(player.transform.position, pawn.transform.position);

        // Clamp that distance between our zero lead and max lead distances
        distanceToPlayer = Mathf.Clamp(distanceToPlayer, noLeadDistance, maxLeadDistance);

        float dTPFromMin = distanceToPlayer - noLeadDistance;
        float totalLeadDistanceRange = maxLeadDistance - noLeadDistance;
        // Find what percent of the total range I currently am
        float percentOfLeadDistanceRange = dTPFromMin / totalLeadDistanceRange;

        // Now that I have that range, I can multiply my lead modifier by it
        // Find a few "seconds" in front of the player
        leadVector = player.pawn.anim.velocity * (leadModifier * percentOfLeadDistanceRange);
    }

    public void ShootAtPlayer()
    {
        // Find distance to the player
        float distanceToPlayer = Vector3.Distance(player.transform.position, pawn.transform.position);
        
        if (distanceToPlayer > minShootDistance && distanceToPlayer < maxShootDistance)
        {
            float shootingError = Random.Range(-maxShootingError * 0.5f, maxShootingError * 0.5f);

            // Rotate weapon for error
            pawn.weapon.transform.Rotate(0, shootingError, 0);
            // Shoot
            pawn.weapon.Attack();
            // The projectile script will handle the rest
            // Rotate back
            pawn.weapon.transform.Rotate(0, -shootingError, 0);
        }
    }

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void RotateTowardsPlayer()
    {
        // Find vector to player
        Vector3 vectorToPlayer = (player.pawn.transform.position + leadVector) - pawn.transform.position;
        // Find quarternion that is looking down that vector
        Quaternion lookRotation = Quaternion.LookRotation(vectorToPlayer, Vector3.up);
        // Get rotation speed from pawn
        pawn.transform.rotation = Quaternion.RotateTowards(pawn.transform.rotation, lookRotation, pawn.rotateSpeed * Time.deltaTime);
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
    }

    private void OnAnimatorMove()
    {
        // This runs everytime the animator causes us to move in position
        // Tell the NavMesh Agent that we already moved (so it doesn't have to)
        agent.velocity = pawn.anim.velocity;
    }
}
