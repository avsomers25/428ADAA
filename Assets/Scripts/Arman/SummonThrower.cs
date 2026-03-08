using UnityEngine;
using TMPro;

public class SummonThrower : MonoBehaviour
{
    public GameObject[] throwableObjects;
    public Transform throwPoint;
    public float throwForce = 30f;

    public TextMeshProUGUI selectedObjectText;

    int currentObject = 0;

    void Start()
    {
        UpdateSelectedText();
    }

    void Update()
    {
        // Press 1 -> previous object
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentObject--;
            if (currentObject < 0)
                currentObject = throwableObjects.Length - 1;

            UpdateSelectedText();
        }

        // Press 2 -> next object
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentObject++;
            if (currentObject >= throwableObjects.Length)
                currentObject = 0;

            UpdateSelectedText();
        }

        // Throw object
        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowObject();
        }
    }

    void ThrowObject()
    {
        if (throwableObjects.Length == 0) return;

        GameObject obj = Instantiate(
            throwableObjects[currentObject],
            throwPoint.position,
            throwPoint.rotation
        );

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }
    }

    void UpdateSelectedText()
    {
        if (selectedObjectText != null && throwableObjects.Length > 0)
        {
            selectedObjectText.text = "Selected: " + throwableObjects[currentObject].gameObject.name;
        }
    }
}