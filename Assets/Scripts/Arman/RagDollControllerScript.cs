using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Animator animator;

    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private CrowdAgent crowdAgent;

    void Awake()
    {
        animator = GetComponent<Animator>();   // auto find animator

        ragdollBodies = GetComponentsInChildren<Rigidbody>(true);
        ragdollColliders = GetComponentsInChildren<Collider>(true);
        crowdAgent = GetComponent<CrowdAgent>();

        ForceStandState();
    }


    private void Start()
    {
        ForceStandState();
    }

    private void OnEnable()
    {
        if (ragdollBodies == null || ragdollBodies.Length == 0)
            ragdollBodies = GetComponentsInChildren<Rigidbody>(true);

        if (ragdollColliders == null || ragdollColliders.Length == 0)
            ragdollColliders = GetComponentsInChildren<Collider>(true);

        if (crowdAgent == null)
            crowdAgent = GetComponent<CrowdAgent>();

        ForceStandState();
    }

    public void ForceStandState()
    {
        if (animator != null)
            animator.enabled = true;

        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject == gameObject)
                continue;

            if (!rb.isKinematic)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            rb.isKinematic = true;

        }

        if (crowdAgent != null)
        {
            crowdAgent.ResetMovementState();
        }
    }

    public void SetRagdoll(bool enabled)
    {
        if (enabled && crowdAgent != null)
        {
            crowdAgent.StopMovement();
        }

        if (animator != null)
            animator.enabled = !enabled;

        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject == gameObject)
                continue;


            if (!rb.isKinematic || enabled) 
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            rb.isKinematic = !enabled;
        }
    }

    public void EnableRagdoll(Vector3 hitForce, Vector3 hitPoint)
    {
        SetRagdoll(true);

        Rigidbody closest = FindClosestRigidbody(hitPoint);
        if (closest != null)
        {
            closest.AddForceAtPosition(hitForce, hitPoint, ForceMode.Impulse);
        }
    }

    private Rigidbody FindClosestRigidbody(Vector3 point)
    {
        Rigidbody closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject == gameObject)
                continue;

            float dist = Vector3.Distance(rb.worldCenterOfMass, point);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = rb;
            }
        }

        return closest;
    }
}