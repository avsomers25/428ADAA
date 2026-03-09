using UnityEngine;

public class SpawnThrow : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform throwPoint;
    public Transform goal;
    public float throwForce = 20f;
    public KeyCode throwkey;

    void Update()
    {
        if (Input.GetKeyDown(throwkey))
        {
            GameObject proj = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);

            // HARD reset ragdoll first
            RagdollController ragdoll = proj.GetComponent<RagdollController>();
            if (ragdoll != null)
            {
                ragdoll.ForceStandState();
            }

            CrowdAgent crowdAgent = proj.GetComponent<CrowdAgent>();
            if (crowdAgent != null)
            {
                crowdAgent.target = goal;
                crowdAgent.ResetMovementState();
            }

            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
            }
        }
    }
}