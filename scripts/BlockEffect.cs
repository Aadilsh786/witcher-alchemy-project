using UnityEngine;

public class BlockEffect : MonoBehaviour
{
    // Singleton instance accessible via BlockEffect.Instance
    public static BlockEffect Instance;

    [Header("Effects")]
    public ParticleSystem eliminationEffectPrefab;  // Assign your particle prefab in the Inspector
    public AudioClip eliminationSound;              // Assign the elimination sound clip in the Inspector
    public AudioClip dragSound;                       // Assign a drag sound clip if needed
    public AudioClip levelCompleteSound;              // Assign the level complete sound clip

    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure there is only one instance of BlockEffect.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Get or add an AudioSource component.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method to play the elimination particle effect and sound.
    public void PlayEliminationEffect(Vector3 position)
    {
        if (eliminationEffectPrefab != null)
        {
            ParticleSystem effect = Instantiate(eliminationEffectPrefab, position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }

        if (eliminationSound != null)
        {
            audioSource.PlayOneShot(eliminationSound);
        }
    }
}
