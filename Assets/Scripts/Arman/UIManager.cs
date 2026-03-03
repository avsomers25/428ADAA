using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public SummonThrower throwerScript; 
    public Slider forceSlider;
    
    [Header("WASD Visuals")]
    public Image wKey;
    public Image aKey;
    public Image sKey;
    public Image dKey;
    public Image fKey;
    public Image LeftShiftKey;
    public Color normalColor = new Color(1, 1, 1, 0.5f); // Semi-transparent white
    public Color activeColor = Color.green;

    void Start()
    {
        // If you didn't drag it in, try to find it automatically
        if (throwerScript == null)
            throwerScript = FindFirstObjectByType<SummonThrower>();

        if (forceSlider != null && throwerScript != null)
        {
            // Set slider to the current value of the script
            forceSlider.value = throwerScript.throwForce;
            
            // Listen for slider changes
            forceSlider.onValueChanged.AddListener(HandleSliderChange);
        }
    }

    void Update()
    {
        // Highlight logic
        SetKeyColor(KeyCode.W, wKey);
        SetKeyColor(KeyCode.A, aKey);
        SetKeyColor(KeyCode.S, sKey);
        SetKeyColor(KeyCode.D, dKey);
        SetKeyColor(KeyCode.F, fKey);
        SetKeyColor(KeyCode.LeftShift, LeftShiftKey);
    }

    void HandleSliderChange(float value)
    {
        if (throwerScript != null)
        {
            throwerScript.throwForce = value;
        }
    }

    void SetKeyColor(KeyCode key, Image targetImage)
    {
        if (targetImage == null) return;
        targetImage.color = Input.GetKey(key) ? activeColor : normalColor;
    }
}