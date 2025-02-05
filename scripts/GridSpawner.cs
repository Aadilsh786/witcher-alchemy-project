using UnityEngine;
using UnityEngine.UI;

public class GridSpawner : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 9;
    public int columns = 9;
    public GameObject blockPrefab;  // Assign your block prefab here
    public RectTransform container; // Assign the BlockContainer Panel

    private float blockSize;

    void Start()
    {
        if (blockPrefab == null || container == null)
        {
            Debug.LogError("Block Prefab or Container is missing!");
            return;
        }

        SpawnGrid();
    }

    void SpawnGrid()
    {
        RectTransform blockRect = blockPrefab.GetComponent<RectTransform>();
        blockSize = blockRect.rect.width; // Get size of one block

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(
                    col * blockSize,  // Horizontal placement
                    -row * blockSize, // Vertical placement (Start top-down)
                    0
                );

                GameObject newBlock = Instantiate(blockPrefab, container);
                newBlock.GetComponent<RectTransform>().anchoredPosition = position;

                // Enable Rigidbody2D for falling effect
                Rigidbody2D rb = newBlock.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
