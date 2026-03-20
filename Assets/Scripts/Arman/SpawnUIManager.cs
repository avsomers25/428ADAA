using UnityEngine;
using UnityEngine.UI; 

public class SpawnUIManager : MonoBehaviour
{
    public SpawnThrow spawnLogic;
    
    [Header("Colors")]
    public Color activeColor = Color.green;
    public Color defaultColor = Color.white;

    [Header("Button Groups")]
    public Button[] spawnButtons;
    public Button[] goalButtons;

    void Start()
    {
        UpdateButtonVisuals(spawnButtons, 0);
        UpdateButtonVisuals(goalButtons, 0);
    }

    public void SelectSpawn(int index)
    {
        spawnLogic.SetSpawnIndex(index);
        UpdateButtonVisuals(spawnButtons, index);
    }
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