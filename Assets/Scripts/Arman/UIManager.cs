using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public SummonThrower throwerScript; 
    public Slider forceSlider;
    
    [Header("Console Settings")]
    public GameObject linePrefab; // A prefab with TextMeshPro and the ConsoleLine script
    public Transform consoleContainer; // A UI Panel with a Vertical Layout Group

    [Header("WASD Visuals")]
    public Image wKey, aKey, sKey, dKey, fKey, LeftShiftKey, ctrlKey, qKey, eKey;
    public Color normalColor = new Color(1, 1, 1, 0.5f);
    public Color activeColor = Color.green;

    void Start()
    {
        if (throwerScript == null)
            throwerScript = FindFirstObjectByType<SummonThrower>();

        if (forceSlider != null && throwerScript != null)
        {
            forceSlider.value = throwerScript.throwForce;
            forceSlider.onValueChanged.AddListener(HandleSliderChange);
        }
    }

    void Update()
    {
        HandleInput(KeyCode.W, wKey);
        HandleInput(KeyCode.A, aKey);
        HandleInput(KeyCode.S, sKey);
        HandleInput(KeyCode.D, dKey);
        HandleInput(KeyCode.F, fKey);
        HandleInput(KeyCode.Q, qKey);
        HandleInput(KeyCode.E, eKey);
        HandleInput(KeyCode.LeftShift, LeftShiftKey);
        HandleInput(KeyCode.LeftControl, ctrlKey);
    }

    void HandleInput(KeyCode key, Image targetImage)
    {
        if (targetImage == null) return;
        targetImage.color = Input.GetKey(key) ? activeColor : normalColor;

        if (Input.GetKeyDown(key) && (key == KeyCode.LeftShift))
        {
            AddLineToConsole($"DUMMY SPAWN");
        }
    }

    void AddLineToConsole(string message)
    {
        if (linePrefab == null || consoleContainer == null) return;

        // Create the new line UI element
        GameObject newLine = Instantiate(linePrefab, consoleContainer);
        
        // Set the text and timestamp
        string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
        newLine.GetComponentInChildren<TextMeshProUGUI>().text = $"[{timestamp}] {message}";
    }

    void HandleSliderChange(float value)
    {
        if (throwerScript != null)
            throwerScript.throwForce = value;
    }
}