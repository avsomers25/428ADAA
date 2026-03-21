using UnityEngine;
using System.Collections.Generic;

public class SpawnThrow : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    [Header("Location Settings")]
    public Transform[] spawnPoints;
    public Transform[] goalPoints;
    
    [Header("Current Selection")]
    public int currentSpawnIndex = 0;
    public int currentGoalIndex = 1;

    [Header("Settings")]
    public float throwForce = 20f;
    public KeyCode throwKey = KeyCode.LeftShift;

    void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            SpawnAndThrow();
        }
    }

    public void SpawnAndThrow()
    {
        if (spawnPoints.Length == 0 || goalPoints.Length == 0) return;

        Transform activeSpawn = spawnPoints[currentSpawnIndex % spawnPoints.Length];
        Transform activeGoal = goalPoints[currentGoalIndex % goalPoints.Length];

        GameObject proj = Instantiate(projectilePrefab, activeSpawn.position, activeSpawn.rotation);

        RagdollController ragdoll = proj.GetComponent<RagdollController>();
        if (ragdoll != null) ragdoll.ForceStandState();

        CrowdAgent crowdAgent = proj.GetComponent<CrowdAgent>();
        if (crowdAgent != null)
        {
            crowdAgent.target = activeGoal;
            crowdAgent.ResetMovementState();
        }

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(activeSpawn.forward * throwForce, ForceMode.Impulse);
        }
    }
    public void SetSpawnIndex(int index) => currentSpawnIndex = index;
    public void SetGoalIndex(int index) => currentGoalIndex = index;
}