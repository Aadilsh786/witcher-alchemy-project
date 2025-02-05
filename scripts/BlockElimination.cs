using UnityEngine;

public class BlockElimination : MonoBehaviour
{
    public Socket socket;  // The socket where the block is placed

    // Call this method to check and eliminate the block
    public void CheckAndEliminateBlock()
    {
        if (socket.currentBlock != null && socket.currentBlock.isPlaced)
        {
            socket.currentBlock.EliminateBlock();  // Eliminate the block
            socket.currentBlock = null;  // Clear the socket
        }
    }
}
