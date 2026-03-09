using UnityEngine;

public class ClickToRagdoll : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float forceAmount = 12f;

    private void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                RagdollController ragdoll = hit.collider.GetComponentInParent<RagdollController>();
                if (ragdoll != null)
                {
                    Vector3 forceDir = ray.direction.normalized;
                    Vector3 impulse = forceDir * forceAmount;
                    ragdoll.EnableRagdoll(impulse, hit.point);
                }
            }
        }
    }
}