using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BackgroundAudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("The audio clip that will be played in the background.")]
    public AudioClip backgroundAudio;

    [Tooltip("List of scenes where the background audio should stop.")]
    public List<string> scenesToStopAudio;

    private AudioSource audioSource;

    void Start()
    {
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Check if background audio is assigned and play it if set
        if (backgroundAudio != null)
        {
            audioSource.clip = backgroundAudio;
            audioSource.loop = true; // Loop the background audio
            audioSource.Play();
        }

        // Add a listener to the scene-loaded event to check for scene change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only stop audio in scenes listed in the "scenesToStopAudio" list
        if (scenesToStopAudio.Contains(scene.name))
        {
            StopBackgroundAudio();
        }
        else
        {
            // Ensure music continues playing if the scene is not in the stop list
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void StopBackgroundAudio()
    {
        // Stop the audio if it's playing
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene-loaded event when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
