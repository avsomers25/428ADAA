using UnityEngine;
using UnityEngine.AI;

public class CrowdAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target; // Drag a "Goal" object here

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Give the agent a random speed for variety
        agent.speed = Random.Range(3.0f, 5.0f);
        
        if (target != null)
            agent.SetDestination(target.position);
    }
}