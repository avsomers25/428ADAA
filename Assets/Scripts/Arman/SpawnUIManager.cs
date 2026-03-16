using UnityEngine;
using UnityEngine.UI; // Required for the Button and Color components

public class SpawnUIManager : MonoBehaviour
{
    public SpawnThrow spawnLogic; // Drag your object with SpawnThrow here
    
    [Header("Colors")]
    public Color activeColor = Color.green;
    public Color defaultColor = Color.white;

    [Header("Button Groups")]
    public Button[] spawnButtons;
    public Button[] goalButtons;

    void Start()
    {
        // Initialize the UI to show the first buttons as active
        UpdateButtonVisuals(spawnButtons, 0);
        UpdateButtonVisuals(goalButtons, 0);
    }

    // Call this from your Spawn Buttons (On Click)
    public void SelectSpawn(int index)
    {
        spawnLogic.SetSpawnIndex(index);
        UpdateButtonVisuals(spawnButtons, index);
    }

    // Call this from your Goal Buttons (On Click)
    public void SelectGoal(int index)
    {
        spawnLogic.SetGoalIndex(index);
        UpdateButtonVisuals(goalButtons, index);
    }

    private void UpdateButtonVisuals(Button[] group, int activeIndex)
    {
        for (int i = 0; i < group.Length; i++)
        {
            if (i == activeIndex)
            {
                group[i].image.color = activeColor;
            }
            else
            {
                group[i].image.color = defaultColor;
            }
        }
    }
}