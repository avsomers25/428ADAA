using UnityEngine;
using UnityEngine.AI;

public class CrowdAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private Animator animator;

    public Transform target;
    public float avoidanceRadius = 10.0f;

    private bool isWalking = false;
    private bool stopped = false;

    // Matches your Animator Controller parameters exactly
    private readonly string boolParam = "isMoving";
    private readonly string floatParam = "Speed";

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        animator = GetComponent<Animator>();

        if (obstacle != null)
        {
            obstacle.shape = NavMeshObstacleShape.Capsule;
            obstacle.size = new Vector3(avoidanceRadius, 2f, avoidanceRadius);
            obstacle.carving = true;
            obstacle.enabled = false;
        }

        if (agent != null)
            agent.enabled = false;

        isWalking = false;
    }

    void OnEnable()
    {
        ResetMovementState();
    }

    void Update()
    {
        // Logic for finding the NavMesh initially
        if (!isWalking && agent != null && !stopped)
        {
            CheckForNavMesh();
        }

        // NEW: Logic for checking if we have arrived at the target
        if (isWalking && agent != null && agent.enabled && !agent.pathPending)
        {
            CheckDestination();
        }
    }

    void CheckDestination()
    {
        // Checks if the agent is close enough to the destination
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Double check if the agent has a path or is very close to avoid false triggers
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                StopMovement();
            }
        }
    }

    void CheckForNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            ActivateAgent(hit.position);
        }
    }

    void ActivateAgent(Vector3 navMeshPosition)
    {
        if (obstacle != null) obstacle.enabled = false;

        isWalking = true;
        stopped = false; 
        agent.enabled = true;
        agent.Warp(navMeshPosition);
        agent.isStopped = false;
        agent.speed = Random.Range(3.0f, 10.0f);
        
        animator.SetFloat(floatParam, agent.speed);
        animator.SetBool(boolParam, true);

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void StopMovement()
    {
        isWalking = false;
        stopped = true;

        if (agent != null)
        {
            // It is safer to check enabled before setting to false
            if(agent.isActiveAndEnabled) agent.isStopped = true;
            agent.enabled = false; 
        }

        if (obstacle != null)
        {
            obstacle.enabled = true;
        }
        
        if (animator != null)
        {
            animator.SetBool(boolParam, false);
            animator.SetFloat(floatParam, 0f);
        }
    }

    public void ResetMovementState()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (obstacle == null) obstacle = GetComponent<NavMeshObstacle>();
        if (animator == null) animator = GetComponent<Animator>();

        isWalking = false;
        stopped = false;

        if (obstacle != null) obstacle.enabled = false;

        if (agent != null)
        {
            agent.enabled = false;
        }

        if (animator != null)
        {
            animator.SetBool(boolParam, false);
            animator.SetFloat(floatParam, 0f);
        }
    }
}