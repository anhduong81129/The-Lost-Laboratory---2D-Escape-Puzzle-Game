using UnityEngine;
using Inventory.Model; // Import namespace Inventory.Model to access InventorySO and ItemSO classes [cite: 3, 4]

public class _DeskInteraction : MonoBehaviour, IInteractable // Implement interface IInteractable[cite: 1, 2]
{
    [Header("Desk Interaction Settings")]
    [Tooltip("The GameObject that represents the desk.")]
    public bool hasKey = true; // check ìf desk has key

    [Header("Inventory Setup")]
    public InventorySO inventory; // Reference to the InventorySO ScriptableObject to manage the player's inventory
    public ItemSO keyItem;        // Reference to the ItemSO ScriptableObject that represents the key item to be added to the inventory

    public void Interact() // Method called when the player interacts with the desk
    {
        if (hasKey) // check key
        {
            // Add the key item to the player's inventory using the AddItem method of InventorySO
            bool isAdded = inventory.AddItem(keyItem); 
            
            if (isAdded)
            {
                Debug.Log("Found an item"); 
                hasKey = false; // Set hasKey to false to indicate that the key has been taken
            }
        }
        else
        {
            Debug.Log("Nothing Found");
        }
    }
}