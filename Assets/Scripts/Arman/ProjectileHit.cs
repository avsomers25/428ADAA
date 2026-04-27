using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    public float damage = 20f;
    public float impactForceMultiplier = 10f;
    public bool destroyOnHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        DummyHealth health = collision.collider.GetComponentInParent<DummyHealth>();

        if (health != null)
        {
            Vector3 hitPoint = collision.contacts[0].point;

            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 hitForce = rb != null
                ? rb.linearVelocity * impactForceMultiplier
                : transform.forward * impactForceMultiplier;

            health.TakeDamage(damage, hitForce, hitPoint);
        }

        if (destroyOnHit)
            Destroy(gameObject);
    }
}