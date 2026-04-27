using UnityEngine;

public class DummyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private RagdollController ragdollController;
    private HealthBarUI healthBarUI;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        ragdollController = GetComponent<RagdollController>();
        healthBarUI = GetComponentInChildren<HealthBarUI>(true);

        if (healthBarUI != null)
            healthBarUI.SetHealth(1f);
    }

    public void TakeDamage(float damage, Vector3 hitForce, Vector3 hitPoint)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthBarUI != null)
            healthBarUI.SetHealth(currentHealth / maxHealth);

        if (currentHealth <= 0f)
        {
            Die(hitForce, hitPoint);
        }
    }

    private void Die(Vector3 hitForce, Vector3 hitPoint)
    {
        isDead = true;

        if (ragdollController != null)
            ragdollController.EnableRagdoll(hitForce, hitPoint);
    }
}