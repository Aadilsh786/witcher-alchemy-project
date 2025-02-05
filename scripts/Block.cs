using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerClickHandler
{
    public enum BlockColor { Red, Blue, Green, Yellow, Grey }

    [Header("Block Settings")]
    public BlockColor blockColor;
    public int value;
    public bool isPlaced = false;

    [Header("Components")]
    public Image baseSlotImage;
    public Text valueText;

    [Header("Random Color Settings")]
    public bool randomizeImageColor = true;
    public Color[] availableColors = new Color[] 
    { 
        Color.red, 
        Color.yellow, 
        Color.green, 
        Color.blue, 
        Color.grey 
    };

    /// <summary>
    /// Initializes the block with a given color, value, and sprite.
    /// </summary>
    /// <param name="newColor">The block's color.</param>
    /// <param name="newValue">The value for the block.</param>
    /// <param name="baseSprite">The sprite to display.</param>
    public void Initialize(BlockColor newColor, int newValue, Sprite baseSprite)
    {
        // Prevent reinitialization after the block is created
        if (value > 0) return;

        blockColor = newColor;
        value = newValue;

        if (baseSlotImage != null)
        {
            baseSlotImage.sprite = baseSprite;
            baseSlotImage.enabled = true;
            baseSlotImage.color = availableColors[(int)newColor]; // Keep assigned color
        }

        UpdateValueText();
    }

    /// <summary>
    /// Alternate overload: Initializes the block with a given color and sprite.
    /// The block's value is set randomly between 1 and 10.
    /// </summary>
    /// <param name="newColor">The block's color.</param>
    /// <param name="baseSprite">The sprite to display.</param>
    public void Initialize(BlockColor newColor, Sprite baseSprite)
    {
        int randomValue = Random.Range(1, 11);
        Initialize(newColor, randomValue, baseSprite);
    }

    /// <summary>
    /// Updates the UI text to display the current block value.
    /// </summary>
    public void UpdateValueText()
    {
        if (valueText != null)
        {
            valueText.text = value.ToString();
        }
        else
        {
            Debug.LogWarning("ValueText is not assigned in the Inspector!");
        }
    }

    /// <summary>
    /// Called when the block is clicked. Attempts to place the block in a socket.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlaced)
            return;

        if (SocketManager.Instance != null)
        {
            SocketManager.Instance.TryPlaceBlock(this);
        }
        else
        {
            Debug.LogWarning("SocketManager instance not found.");
        }
    }

    /// <summary>
    /// Reduces the block's value by the specified amount and updates the UI.
    /// If the value reaches 0, the block is eliminated.
    /// </summary>
    /// <param name="amount">The amount to reduce.</param>
    public void ReduceValue(int amount)
    {
        if (value <= 0)
            return;

        value -= amount;
        if (value < 0)
            value = 0;
        UpdateValueText();

        if (value == 0)
        {
            EliminateBlock();
        }
    }

    /// <summary>
    /// Plays an elimination effect (if available) and destroys the block.
    /// </summary>
    public void EliminateBlock()
    {
        if (BlockEffect.Instance != null)
        {
            BlockEffect.Instance.PlayEliminationEffect(transform.position);
        }
        else
        {
            Debug.LogWarning("BlockEffect Instance is not set.");
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Returns a random color from the available colors array.
    /// </summary>
    private Color GetRandomColor()
    {
        if (availableColors == null || availableColors.Length == 0)
            return Color.white;

        return availableColors[Random.Range(0, availableColors.Length)];
    }
}
