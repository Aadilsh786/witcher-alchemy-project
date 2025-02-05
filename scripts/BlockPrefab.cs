using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BlockPrefab : MonoBehaviour
{
    public Image overlayImage;  // The UI Image component for the overlay icon
    public List<Sprite> availableIcons = new List<Sprite>();  // List of possible overlay icons

    void Start()
    {
        AssignRandomOverlay();
    }

    void AssignRandomOverlay()
    {
        if (availableIcons.Count > 0 && overlayImage != null)
        {
            overlayImage.sprite = availableIcons[Random.Range(0, availableIcons.Count)];
            overlayImage.enabled = true;  // Make sure the icon is visible
        }
        else
        {
            Debug.LogError("No available icons or overlayImage is missing!");
        }
    }
}
