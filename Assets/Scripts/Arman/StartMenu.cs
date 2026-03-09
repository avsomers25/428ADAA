using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameplayUI; // optional, only if you want HUD hidden at start

    void Start()
    {
        Time.timeScale = 0f; // pause game when menu is open

        if (menuUI != null)
            menuUI.SetActive(true);

        if (gameplayUI != null)
            gameplayUI.SetActive(false);
    }

    public void PlayGame()
    {
        if (menuUI != null)
            menuUI.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        Time.timeScale = 1f; // resume game
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
}