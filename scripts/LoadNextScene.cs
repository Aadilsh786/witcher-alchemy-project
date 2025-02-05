using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour
{
    [Tooltip("The name of the scene to load.")]
    public string nextScene;

    // This function will be exposed to the OnClick in the Inspector
    public void OnButtonClick()
    {
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
