using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneManager : MonoBehaviour
{
    [Header("Video Settings")]
    [Tooltip("The Video Player component.")]
    public VideoPlayer videoPlayer;

    [Tooltip("The name of the next scene to load.")]
    public string nextScene;

    [Header("UI Elements")]
    [Tooltip("The skip button to be shown after 2 seconds.")]
    public Button skipButton;

    [Tooltip("Time (in seconds) before the skip button appears.")]
    public float skipButtonDelay = 2f;

    private void Start()
    {
        // Play the video immediately as the scene starts
        videoPlayer.Play();

        // Initially hide the skip button
        skipButton.gameObject.SetActive(false);

        // Start the coroutine to show the skip button after a delay
        StartCoroutine(ShowSkipButtonAfterDelay());
    }

    private IEnumerator ShowSkipButtonAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(skipButtonDelay);

        // Make the skip button visible after the delay
        skipButton.gameObject.SetActive(true);

        // Add the listener to the skip button to switch scenes when clicked
        skipButton.onClick.AddListener(SkipVideo);
    }

    private void SkipVideo()
    {
        // Load the next scene
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("Next scene is not specified!");
        }
    }
}
