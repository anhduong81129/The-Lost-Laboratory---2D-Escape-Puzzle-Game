using UnityEngine;
using Inventory.Model;

public class DoorController : MonoBehaviour, IInteractable
{
    [Header("Door Sprites")]
    public Sprite lockedSprite;  // image of the door when it's locked
    public Sprite openedSprite;  // image of the door when it's opened

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D doorCollider;

    [Header("Requirements")]
    // Data of key to open door
    public ItemSO requiredKey;
    public InventorySO playerInventory; 


    private bool isOpened = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        
        // Set the initial sprite to locked
        spriteRenderer.sprite = lockedSprite;
    }

    // Method to handle interaction with the door
    public void Interact()
    {
        // If the door is already opened, do nothing
        if (isOpened) return;

        // Kiểm tra xem đã gán Inventory trên Inspector chưa
        if (playerInventory == null)
        {
            Debug.LogError("Chưa gán PlayerInventory vào Inspector của Doors!");
            return;
        }

        // Check if the player has the required key in their inventory
        if (playerInventory.ContainsItem(requiredKey)) 
        {
            OpenDoor();
            
            // (Tùy chọn) Xóa chìa khóa khỏi túi đồ sau khi dùng
            playerInventory.RemoveItem(requiredKey); 
        }
        else
        {
            Debug.Log("The door is locked. You need the correct key to open it.");
        }
    }

    private void OpenDoor()
    {
        isOpened = true;
        
        if (spriteRenderer != null && openedSprite != null)
        {
            spriteRenderer.sprite = openedSprite;
        }
        if (doorCollider != null)
        {
            doorCollider.enabled = false; // Tắt collider để player đi qua cửa
        }
        Debug.Log("Door opened successfully!");
    }
}