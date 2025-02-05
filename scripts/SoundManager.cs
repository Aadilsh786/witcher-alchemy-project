using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip blockEliminateSound;
    public AudioClip blockDragSound;
    public AudioClip levelCompleteSound;

    // Method to play the sound for block elimination
    public void PlayBlockEliminateSound()
    {
        AudioSource.PlayClipAtPoint(blockEliminateSound, Camera.main.transform.position);
    }

    // Method to play the sound when a block is dragged
    public void PlayBlockDragSound()
    {
        AudioSource.PlayClipAtPoint(blockDragSound, Camera.main.transform.position);
    }

    // Method to play the sound for completing the level
    public void PlayLevelCompleteSound()
    {
        AudioSource.PlayClipAtPoint(levelCompleteSound, Camera.main.transform.position);
    }
}
