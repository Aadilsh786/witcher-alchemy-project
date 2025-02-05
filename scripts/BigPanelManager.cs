using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigPanelManager : MonoBehaviour
{
    public static BigPanelManager Instance;

    [Header("Big Panel Settings")]
    public GameObject blockPrefab;  
    public Transform bigPanel;      
    public int columns = 10;          // 10 columns
    public int rows = 5;              // 5 rows
    public float blockSpacing = 1.1f;
    public int refillCount = 5;       // Rows to refill

    [Header("Color & Icon Options")]
    public List<Block.BlockColor> availableColors;  
    public Sprite[] availableIcons;

    private Block[,] grid;
    public List<Block> allBlocks = new List<Block>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        grid = new Block[columns, rows];
        InitializeGrid();
    }

    // Method to initialize blocks with connected colors in each row.
    public void InitializeGrid()
    {
        for (int y = 0; y < rows; y++)
        {
            int x = 0;

            // Fill each row with connected blocks
            while (x < columns)
            {
                // Randomly decide group size (4, 5, or 7 blocks connected)
                int groupSize = Random.Range(4, 8);
                groupSize = Mathf.Min(groupSize, columns - x);  // Ensure the group fits in the row

                // Randomly pick a color for this group of connected blocks
                Block.BlockColor chosenColor = availableColors[Random.Range(0, availableColors.Count)];

                // Spawn the connected blocks of the same color in the current row
                for (int i = 0; i < groupSize; i++)
                {
                    SpawnBlockAt(x + i, y, chosenColor);
                }

                x += groupSize;  // Move x position for the next group
            }
        }
    }

    // Spawn a block at a specific position with a specified color.
    void SpawnBlockAt(int col, int row, Block.BlockColor chosenColor)
    {
        Vector3 pos = new Vector3(col * blockSpacing, row * blockSpacing, 0);
        GameObject newBlockObj = Instantiate(blockPrefab, pos, Quaternion.identity, bigPanel);
        Block block = newBlockObj.GetComponent<Block>();

        if (block != null)
        {
            // Randomize the number and icon
            int randomNumber = Random.Range(1, 11);  // Random value between 1 and 10
            Sprite randomIcon = availableIcons[Random.Range(0, availableIcons.Length)];

            // Call the Initialize method with three parameters.
            block.Initialize(chosenColor, randomNumber, randomIcon);

            // Add the block to the grid and allBlocks list.
            grid[col, row] = block;
            allBlocks.Add(block);

            // Assign tag for verification.
            newBlockObj.tag = "BlockIngredient";
        }
    }

    public void ShiftDownBlocks()
    {
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if (grid[col, row] == null)
                {
                    for (int above = row + 1; above < rows; above++)
                    {
                        if (grid[col, above] != null)
                        {
                            Block block = grid[col, above];
                            grid[col, row] = block;
                            grid[col, above] = null;
                            block.transform.position = new Vector3(col * blockSpacing, row * blockSpacing, 0);
                            break;
                        }
                    }
                }
            }
        }
        StartCoroutine(RefillTopRows());
    }

    IEnumerator RefillTopRows()
    {
        yield return new WaitForSeconds(0.3f);
        for (int col = 0; col < columns; col++)
        {
            for (int row = rows - refillCount; row < rows; row++)
            {
                if (grid[col, row] == null)
                {
                    SpawnBlockAt(col, row, availableColors[Random.Range(0, availableColors.Count)]);
                }
            }
        }
    }

    public bool IsPanelEmpty()
    {
        foreach (Block block in grid)
        {
            if (block != null) return false;
        }
        return true;
    }

    /// <summary>
    /// Removes a block from the big panel (both grid & list) and updates the score.
    /// </summary>
    public void UnregisterBlock(Block block)
    {
        if (block == null) return;

        // Find and remove the block from the grid.
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if (grid[col, row] == block)
                {
                    grid[col, row] = null;
                    break;
                }
            }
        }

        // Remove the block from the allBlocks list.
        allBlocks.Remove(block);

        // Trigger score update here: increment score by 1 per block eliminated.
        GameManager.Instance.UpdateScore(1);
    }

    public List<Block> GetMatchingBlocks(Block.BlockColor color)
    {
        List<Block> matches = new List<Block>();
        foreach (Block b in allBlocks)
        {
            if (b != null && b.blockColor == color)
                matches.Add(b);
        }
        return matches;
    }
}
