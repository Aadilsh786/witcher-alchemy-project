using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Singleton instance

    [Header("Game Settings")]
    public BigPanelManager bigPanelManager;
    public SocketManager socketManager;
    public SelectionManager selectionManager;

    [Header("UI Elements")]
    public GameObject winPopup;        // The panel that shows when level is complete
    public Text scoreText;             // Display score
    public Slider progressIndicator;  // The progress bar (Slider)
    public GameObject gameOverPanel;   // The Game Over panel
    public Text timerText;             // Reference to display the timer

    [Header("Score Settings")]
    public int score = 0;
    public int moves = 0;
    public int blocksEliminated = 0;
    public int maxBlocks = 50; // Slot to set the maximum number of blocks to be eliminated in the level

    private float timer = 0f;         // Timer to track time elapsed
    private bool gameOver = false;    // Whether the game is over
    private bool levelComplete = false;  // Flag to track if the level is complete

    void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    void Start()
    {
        // Initialize/reset values at the start
        score = 0;
        moves = 0;
        blocksEliminated = 0;
        timer = 0f;
        gameOver = false;
        levelComplete = false;

        winPopup.SetActive(false);
        gameOverPanel.SetActive(false);

        // Set the maximum value of the progress slider (progress bar)
        progressIndicator.maxValue = maxBlocks;
        progressIndicator.value = 0;  // Start progress at 0
    }

    private void Update()
    {
        // If the game is not over and not yet level complete, track the time
        if (!gameOver && !levelComplete)
        {
            timer += Time.deltaTime;

            // If 10 seconds have passed without a block being placed, show the Game Over panel
            if (timer >= 5f)
            {
                TriggerGameOver();
            }

            // Update the timer display
            timerText.text = "Time: " + Mathf.Round(timer).ToString() + "s";
        }
    }

    // This method will reset the timer whenever a block is successfully placed
    public void ResetTimer()
    {
        timer = 0f;
    }

    // This method will update the score whenever a block is eliminated
    public void UpdateScore(int eliminatedThisMove)
    {
        blocksEliminated += eliminatedThisMove;
        moves++;
        score = blocksEliminated * 10 - moves * 5;  // Adjust weights as desired

        // Update UI elements
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        // Update progress indicator (Slider) based on blocks eliminated
        if (progressIndicator != null)
        {
            progressIndicator.value = blocksEliminated;
        }

        // If the blocks eliminated surpass the maxBlocks, complete the level
        if (blocksEliminated >= maxBlocks && !levelComplete)
        {
            LevelComplete();
        }
    }

    // Called when the level is complete (all blocks are eliminated or maxBlocks surpassed)
    void LevelComplete()
    {
        // Prevent the level complete from being triggered multiple times
        if (levelComplete) return;

        levelComplete = true;  // Mark the level as complete
        timer = 0f;             // Stop the timer when level completes

        // Play level complete sound:
        if (BlockEffect.Instance.levelCompleteSound != null)
        {
            AudioSource.PlayClipAtPoint(BlockEffect.Instance.levelCompleteSound, Camera.main.transform.position);
        }

        winPopup.SetActive(true);  // Show the win popup
    }

    // Called to trigger the Game Over panel when time runs out
    private void TriggerGameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    // Retry the level (called by the Retry button in the Game Over Panel)
    public void RetryLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit the game (called by the Quit button)
    public void QuitGame()
    {
        Application.Quit();
    }
}
