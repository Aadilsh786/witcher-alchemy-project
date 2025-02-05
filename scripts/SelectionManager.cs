using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [Header("Selection Settings")]
    public GameObject blockPrefab;  // Prefab for selection blocks
    public Transform[] spawnPoints; // Positions where blocks spawn

    [Header("UI Container")]
    [Tooltip("Assign the panel (or container transform) in which the blocks should appear.")]
    public Transform selectionPanel;

    [Header("Block Options")]
    public Block.BlockColor[] availableColors; 
    public Sprite[] availableIcons;

    void Start()
    {
        if (blockPrefab == null)
        {
            Debug.LogError("SelectionManager: blockPrefab is not assigned!");
            return;
        }

        if (selectionPanel == null)
        {
            Debug.LogError("SelectionManager: selectionPanel (container) is not assigned!");
            return;
        }

        GenerateNewBlocks();
    }

    // Generate one block per spawn point
    public void GenerateNewBlocks()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint == null) continue;

            // Instantiate the block inside the selection panel
            GameObject newBlock = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity, selectionPanel);
            Block blockScript = newBlock.GetComponent<Block>();

            if (blockScript == null)
            {
                Debug.LogError("SelectionManager: Block script is missing on the prefab!");
                continue;
            }

            // Assign fixed color, icon, and value
            int colorIndex = Random.Range(0, availableColors.Length);
            int iconIndex = Random.Range(0, availableIcons.Length);
            int randomValue = Random.Range(1, 10);

            blockScript.Initialize(availableColors[colorIndex], randomValue, availableIcons[iconIndex]);

            // ✅ Ensuring the text updates properly
            if (blockScript.valueText != null)
            {
                blockScript.valueText.text = randomValue.ToString();
            }
        }
    }
}
