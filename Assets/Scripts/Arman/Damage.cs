using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public float damage = 25f;
    public float impactForceMultiplier = 10f;
    public bool destroyOnHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        DummyHealth health = collision.collider.GetComponentInParent<DummyHealth>();

        if (health != null)
        {
            Vector3 hitPoint = collision.contacts[0].point;

            Rigidbody myRb = GetComponent<Rigidbody>();
            Vector3 hitForce = myRb != null
                ? myRb.linearVelocity * impactForceMultiplier
                : transform.forward * impactForceMultiplier;

            health.TakeDamage(damage, hitForce, hitPoint, true);
        }

        if (destroyOnHit)
            Destroy(gameObject);
    }
}