using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFadeLoop : MonoBehaviour
{
    [Header("Text Fade Settings")]
    [Tooltip("The Text component to fade.")]
    public Text textComponent;

    [Tooltip("Duration for fade in/out (in seconds).")]
    public float fadeDuration = 1f;

    [Tooltip("Wait time before starting the next fade cycle.")]
    public float waitTime = 0.5f;

    private void Start()
    {
        // Start the fade in/out loop when the script starts
        if (textComponent != null)
        {
            StartCoroutine(FadeTextLoop());
        }
        else
        {
            Debug.LogWarning("Text component is not assigned!");
        }
    }

    IEnumerator FadeTextLoop()
    {
        // Loop the fade in and fade out indefinitely
        while (true)
        {
            yield return StartCoroutine(FadeText(1f)); // Fade In
            yield return new WaitForSeconds(waitTime); // Wait before fading out
            yield return StartCoroutine(FadeText(0f)); // Fade Out
            yield return new WaitForSeconds(waitTime); // Wait before fading in
        }
    }

    IEnumerator FadeText(float targetAlpha)
    {
        float startAlpha = textComponent.color.a;
        float elapsedTime = 0f;

        // Get the current color of the text
        Color startColor = textComponent.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        // Smoothly transition the text alpha from start to target
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textComponent.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure the final target alpha is set
        textComponent.color = targetColor;
    }
}
