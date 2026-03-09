using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    public float impactForceMultiplier = 10f;
    public bool destroyOnHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        RagdollController ragdoll = collision.collider.GetComponentInParent<RagdollController>();

        if (ragdoll != null)
        {
            Vector3 hitPoint = collision.contacts[0].point;

            Rigidbody myRb = GetComponent<Rigidbody>();
            Vector3 hitForce = Vector3.zero;

            if (myRb != null)
            {
                hitForce = myRb.linearVelocity * impactForceMultiplier;
            }
            else
            {
                hitForce = transform.forward * impactForceMultiplier;
            }

            ragdoll.EnableRagdoll(hitForce, hitPoint);
        }

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}