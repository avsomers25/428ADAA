using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;

    public void SetHealth(float normalizedValue)
    {
        normalizedValue = Mathf.Clamp01(normalizedValue);

        if (fillImage != null)
            fillImage.fillAmount = normalizedValue;
    }
}