using UnityEngine;
using UnityEngine.AI;

public class CrowdAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    private bool isWalking = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

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
        if (!isWalking && agent != null)
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

        if (agent != null && agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
    }

    public void ResetMovementState()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        isWalking = false;

        if (agent != null)
        {
            agent.enabled = false;
            agent.isStopped = false;
        }
    }
}