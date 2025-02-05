using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Header("Scene Control")]
    [Tooltip("Number of seconds before switching to the next scene.")]
    public float delayBeforeSwitch = 5f;

    [Tooltip("Name of the scene to load next.")]
    public string nextSceneName;

    private float timer;

    void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if the timer has reached the delay time
        if (timer >= delayBeforeSwitch)
        {
            // Load the specified scene
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not specified!");
        }
    }
}
