using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusicManager : MonoBehaviour
{
    [Header("Music Settings")]
    [Tooltip("The AudioClip to play in the background.")]
    public AudioClip backgroundMusic;

    [Tooltip("List of scenes where the background music should stop.")]
    public string[] scenesToStopMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure that this GameObject persists across scene loads
        DontDestroyOnLoad(gameObject);

        // If there's already an instance of the MusicManager, destroy this one to ensure only one instance exists
        if (FindObjectsOfType<PersistentMusicManager>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // If no AudioSource component is found, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Play the background music if it's set
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // Loop the music
            audioSource.Play();
        }

        // Add a listener to the scene loaded event to check for scene change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is in the list of scenes where we should stop music
        foreach (string sceneName in scenesToStopMusic)
        {
            if (scene.name == sceneName)
            {
                StopBackgroundMusic();
                return;
            }
        }

        // If the scene is not in the list, continue playing the music
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void StopBackgroundMusic()
    {
        // Stop the background music if it's playing
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
