using UnityEngine;

public class Socket : MonoBehaviour
{
    public Block currentBlock;  // The block currently placed in this socket

    /// <summary>
    /// Returns true if the socket has no block.
    /// </summary>
    public bool IsEmpty()
    {
        return currentBlock == null;
    }

    /// <summary>
    /// Places the given block into this socket.
    /// Sets the block's parent (so it appears inside the socket), resets its local position and scale,
    /// and marks it as placed.
    /// </summary>
    /// <param name="block">The block to place.</param>
    public void PlaceBlock(Block block)
    {
        if (IsEmpty())
        {
            currentBlock = block;
            // Reparent the block so that it becomes a child of this socket.
            block.transform.SetParent(transform, false);
            // Reset its local position and scale.
            block.transform.localPosition = Vector3.zero;
            block.transform.localScale = Vector3.one;
            block.isPlaced = true;
        }
        else
        {
            Debug.Log("Socket is already occupied!");
        }
    }

    /// <summary>
    /// (Optional) Checks and eliminates the block in this socket if conditions are met.
    /// </summary>
    public void CheckAndEliminate()
    {
        if (currentBlock != null && currentBlock.isPlaced)
        {
            currentBlock.EliminateBlock();
            currentBlock = null;
        }
    }
}
