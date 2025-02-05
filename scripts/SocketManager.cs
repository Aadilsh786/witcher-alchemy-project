using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SocketManager : MonoBehaviour
{
    public static SocketManager Instance; // Singleton instance

    [Header("Socket Settings")]
    public Socket[] allSockets;  // Assign socket GameObjects (with Socket.cs attached)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Places a block in the first available socket and starts elimination.
    /// </summary>
    public void TryPlaceBlock(Block block)
    {
        if (block == null || block.isPlaced)
            return;

        foreach (Socket socket in allSockets)
        {
            if (socket != null && socket.IsEmpty())
            {
                socket.PlaceBlock(block);
                block.isPlaced = true;  // Mark block as placed

                StartCoroutine(ProcessElimination(socket, block));  // Start elimination process

                // Reset the timer whenever a block is placed successfully
                GameManager.Instance.ResetTimer();  
                return;
            }
        }

        Debug.Log("No empty socket available.");
    }

    /// <summary>
    /// Processes elimination of blocks after placement.
    /// </summary>
    private IEnumerator ProcessElimination(Socket socket, Block socketBlock)
    {
        int remainingValue = socketBlock.value;
        int eliminatedThisMove = 0;  

        while (remainingValue > 0)
        {
            List<Block> matchingBlocks = BigPanelManager.Instance.GetMatchingBlocks(socketBlock.blockColor);

            if (matchingBlocks.Count == 0)
            {
                Debug.Log("No more matching blocks left.");
                break;
            }

            foreach (Block targetBlock in matchingBlocks)
            {
                if (targetBlock != null && targetBlock.gameObject.CompareTag("BlockIngredient"))
                {
                    BigPanelManager.Instance.UnregisterBlock(targetBlock);
                    Destroy(targetBlock.gameObject);
                    remainingValue--;  

                    // Increment the number of eliminated blocks
                    eliminatedThisMove++;
                }

                yield return new WaitForSeconds(0.2f);  // Add delay for ASMR effect

                if (remainingValue <= 0)
                    break;
            }
        }

        // Notify GameManager that blocks were eliminated
        if (eliminatedThisMove > 0)
        {
            GameManager.Instance.UpdateScore(eliminatedThisMove);
        }

        // If block value reaches 0, remove it from the socket
        if (remainingValue <= 0)
        {
            RemoveBlockFromSocket(socket);
        }

        // Shift the grid and refill after elimination
        BigPanelManager.Instance.ShiftDownBlocks();
    }

    /// <summary>
    /// Removes a block from the socket after elimination.
    /// </summary>
    private void RemoveBlockFromSocket(Socket socket)
    {
        if (socket.currentBlock != null)
        {
            Destroy(socket.currentBlock.gameObject);  // Destroy the block
            socket.currentBlock = null;  
        }
    }
}
