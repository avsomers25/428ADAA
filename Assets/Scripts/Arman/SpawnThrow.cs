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
    public int currentGoalIndex = 0;

    [Header("Circle Settings")]
    public int spawnCount = 10;
    public float circleRadius = 3f;

    [Header("Force Settings")]
    public float minThrowForce = 15f;
    public float maxThrowForce = 25f;
    public KeyCode throwKey = KeyCode.LeftShift;

    void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            SpawnAndThrowCircle();
        }
    }

    public void SpawnAndThrowCircle()
    {
        if (spawnPoints.Length == 0 || goalPoints.Length == 0) return;

        Transform activeSpawn = spawnPoints[currentSpawnIndex % spawnPoints.Length];
        Transform activeGoal = goalPoints[currentGoalIndex % goalPoints.Length];

        for (int i = 0; i < spawnCount; i++)
        {
            // 1. Calculate radial position
            float angle = i * Mathf.PI * 2f / spawnCount;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * circleRadius;
            Vector3 spawnPos = activeSpawn.position + offset;

            // 2. Instantiate
            GameObject proj = Instantiate(projectilePrefab, spawnPos, activeSpawn.rotation);

            // 3. Setup Components (Ragdoll/Agent)
            RagdollController ragdoll = proj.GetComponent<RagdollController>();
            if (ragdoll != null) ragdoll.ForceStandState();

            CrowdAgent crowdAgent = proj.GetComponent<CrowdAgent>();
            if (crowdAgent != null)
            {
                crowdAgent.target = activeGoal;
                crowdAgent.ResetMovementState();
            }

            // 4. Apply Physics with varying velocity
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // Randomize the force for each dummy
                float randomForce = Random.Range(minThrowForce, maxThrowForce);
                
                // Option A: Throw them all Forward
                rb.AddForce(activeSpawn.forward * randomForce, ForceMode.Impulse);

                // Option B: If you want them to explode OUTWARD from the circle center instead, 
                // use: rb.AddForce(offset.normalized * randomForce, ForceMode.Impulse);
            }
        }
    }

    public void SetSpawnIndex(int index) => currentSpawnIndex = index;
    public void SetGoalIndex(int index) => currentGoalIndex = index;
}