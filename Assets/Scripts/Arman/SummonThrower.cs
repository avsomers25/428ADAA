using UnityEngine;

public class SummonThrower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform throwPoint;
    public float throwForce = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject proj = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);

            Rigidbody rb = proj.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
            }
        }
    }
}