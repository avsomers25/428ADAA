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
            proj.GetComponent<CrowdAgent>().target = goal;

            Rigidbody rb = proj.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
            }
        }
    }
}