using UnityEngine;
using UnityEngine.AI;

public class CrowdAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target; // Assigned by the SpawnThrow script
    private bool isWalking = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // Ensure the agent is off while flying
        if (agent != null) agent.enabled = false; 
    }

    void Update()
    {
        // If we haven't started walking yet, check if we've landed
        if (!isWalking && agent != null)
        {
            CheckForNavMesh();
        }
    }

    void CheckForNavMesh()
    {
        // Check if the object is close enough to a valid NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            ActivateAgent(hit.position);
        }
    }

    void ActivateAgent(Vector3 navMeshPosition)
    {
        isWalking = true;
        
        // Enable agent and snap it to the mesh
        agent.enabled = true;
        agent.Warp(navMeshPosition); 
        
        agent.speed = Random.Range(3.0f, 5.0f);

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}