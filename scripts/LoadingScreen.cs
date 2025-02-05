using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("Loading Screen Settings")]
    [Tooltip("The name of the scene to load.")]
    public string sceneToLoad;

    [Tooltip("Time (in seconds) to show the loading screen.")]
    public float loadingDuration = 2f;

    [Header("UI Elements")]
    [Tooltip("Reference to the slider UI element to reflect loading progress.")]
    public Slider loadingSlider;

    private float progress = 0f;

    void Start()
    {
        // Start the coroutine to load the scene with the loading screen
        StartCoroutine(LoadSceneWithDelay());
    }

    private IEnumerator LoadSceneWithDelay()
    {
        // Simulate loading over the specified duration
        float elapsedTime = 0f;

        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            progress = Mathf.Clamp01(elapsedTime / loadingDuration); // Progress from 0 to 1
            UpdateSlider(progress); // Update the slider UI
            yield return null;
        }

        // Load the next scene asynchronously
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
            asyncLoad.allowSceneActivation = true; // Allow the new scene to activate once fully loaded
        }
        else
        {
            Debug.LogWarning("Scene name is not specified in the inspector!");
        }
    }

    private void UpdateSlider(float value)
    {
        if (loadingSlider != null)
        {
            loadingSlider.value = value; // Update slider progress
        }
    }
}
