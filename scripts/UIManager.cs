using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject levelCompletePanel;  // Panel that shows after the level is completed
    public Text scoreText;  // Text field to display the score
    public Button retryButton;  // Button to retry the level
    public Button quitButton;  // Button to quit the game

    // Method to show the level completion screen
    public void ShowLevelCompleteScreen(int score)
    {
        levelCompletePanel.SetActive(true);  // Show the completion panel
        scoreText.text = "Score: " + score.ToString();  // Update the score display
        retryButton.onClick.AddListener(RestartLevel);  // Set up retry button
        quitButton.onClick.AddListener(QuitGame);  // Set up quit button
    }

    // Method to restart the current level
    public void RestartLevel()
    {
        // Implement restart logic (e.g., reload the scene or reset game state)
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();  // Quit the game
    }
}
