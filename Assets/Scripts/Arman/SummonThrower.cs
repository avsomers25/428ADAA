using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SummonThrower : MonoBehaviour
{
    [Header("Throwing")]
    public GameObject[] throwableObjects;
    public Sprite[] objectIcons;
    public Transform throwPoint;
    public float throwForce = 30f;

    [Header("UI")]
    public TextMeshProUGUI selectedObjectText;
    public GameObject selectionPanel;
    public Transform buttonContainer;
    public GameObject objectButtonPrefab;

    private int currentObject = 0;
    private GameObject heldObject;
    private Rigidbody heldRb;
    private bool menuOpen = false;

    void Start()
    {
        UpdateSelectedText();
        SpawnHeldObject();
        BuildObjectMenu();

        if (selectionPanel != null)
            selectionPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMenu();
        }

        if (menuOpen)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowObject();
        }
    }

    void ToggleMenu()
    {
        menuOpen = !menuOpen;

        if (selectionPanel != null)
            selectionPanel.SetActive(menuOpen);

        Cursor.lockState = menuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = menuOpen;
    }

    void BuildObjectMenu()
    {
        if (buttonContainer == null || objectButtonPrefab == null) return;

        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < throwableObjects.Length; i++)
        {
            GameObject buttonObj = Instantiate(objectButtonPrefab, buttonContainer);

            ObjectSlotUI slot = buttonObj.GetComponent<ObjectSlotUI>();
            if (slot != null)
            {
                Sprite icon = null;
                if (objectIcons != null && i < objectIcons.Length)
                    icon = objectIcons[i];

                slot.Setup(i, throwableObjects[i].name, icon, this);
            }
        }
    }

    public void SelectObject(int index)
    {
        if (index < 0 || index >= throwableObjects.Length) return;

        currentObject = index;
        UpdateSelectedText();
        SpawnHeldObject();

        menuOpen = false;

        if (selectionPanel != null)
            selectionPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SpawnHeldObject()
    {
        if (throwableObjects.Length == 0) return;

        if (heldObject != null)
        {
            Destroy(heldObject);
        }

        heldObject = Instantiate(
            throwableObjects[currentObject],
            throwPoint.position,
            throwPoint.rotation,
            throwPoint
        );

        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        heldRb = heldObject.GetComponent<Rigidbody>();

        if (heldRb != null)
        {
            heldRb.isKinematic = true;
            heldRb.useGravity = false;
        }

        Collider[] cols = heldObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }
    }

    void ThrowObject()
    {
        if (heldObject == null) return;

        heldObject.transform.parent = null;

        if (heldRb != null)
        {
            heldRb.isKinematic = false;
            heldRb.useGravity = true;
            heldRb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }

        Collider[] cols = heldObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }

        heldObject = null;
        heldRb = null;

        SpawnHeldObject();
    }

    void UpdateSelectedText()
    {
        if (selectedObjectText != null && throwableObjects.Length > 0)
        {
            selectedObjectText.text = "Selected: " + throwableObjects[currentObject].name;
        }
    }
}