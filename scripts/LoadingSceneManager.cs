using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    [Header("Loading Settings")]
    [Tooltip("Time in seconds before the button appears.")]
    public float loadingDuration = 4f;

    [Tooltip("The scene to load when the button is clicked.")]
    public string nextScene;

    [Header("UI Elements")]
    [Tooltip("The loading progress bar (Slider).")]
    public Slider progressBar;

    [Tooltip("The button to proceed (Initially disabled).")]
    public Button continueButton;

    private void Start()
    {
        // Ensure the button is disabled at the start
        continueButton.gameObject.SetActive(false);

        // Start the loading process
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / loadingDuration; // Update the progress bar
            yield return null;
        }

        // Enable the continue button after loading is complete
        continueButton.gameObject.SetActive(true);
    }

    // Method to load the next scene when button is clicked
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("Next scene is not set in the inspector!");
        }
    }
}
