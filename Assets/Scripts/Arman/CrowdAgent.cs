using UnityEngine;
using UnityEngine.AI;

public class CrowdAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    
    public Transform target;
    public float avoidanceRadius = 10.0f;

    private bool isWalking = false;
    private bool stopped = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();

        // Configure obstacle for carving
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
        if (!isWalking && agent != null && !stopped)
        {
            CheckForNavMesh();
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
        agent.enabled = true;
        agent.Warp(navMeshPosition);
        agent.isStopped = false;
        agent.speed = Random.Range(3.0f, 5.0f);

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
            agent.enabled = false; // Agent must be off for obstacle to carve properly
        }

        // Enable obstacle to create the avoidance zone
        if (obstacle != null)
        {
            obstacle.enabled = true;
        }
    }

    public void ResetMovementState()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (obstacle == null) obstacle = GetComponent<NavMeshObstacle>();

        isWalking = false;
        stopped = false;

        if (obstacle != null) obstacle.enabled = false;

        if (agent != null)
        {
            agent.enabled = false;
            agent.isStopped = false;
        }
    }
}