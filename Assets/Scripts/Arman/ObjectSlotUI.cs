using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectSlotUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;

    private int objectIndex;
    private SummonThrower summonThrower;

    public void Setup(int index, string objectName, Sprite icon, SummonThrower thrower)
    {
        objectIndex = index;
        summonThrower = thrower;

        if (nameText != null)
            nameText.text = objectName;

        if (iconImage != null)
            iconImage.sprite = icon;
    }

    public void OnClick()
    {
        if (summonThrower != null)
        {
            summonThrower.SelectObject(objectIndex);
        }
    }
}