using UnityEngine;
using Inventory.Model; // Thêm namespace để tham chiếu tới InventorySO

public class PlayerInteractor : MonoBehaviour
{
    [Header("Inventory Setup")]
    [Tooltip("Kéo file PlayerInventory (InventorySO) từ Project vào đây.")]
    public InventorySO playerInventory; // ScriptableObject manage inventory

    [Header("Interaction Settings")]
    [Tooltip("The point from which the player will interact with objects.")]
    public Transform interactPoint; // The point from which the player will interact with objects

    [Tooltip("The radius within which the player can interact with objects.")]
    public float interactRadius = 0.5f; //raius within which the player can interact with objects

    [Tooltip("The layer mask that defines which objects can be interacted with.")]
    public LayerMask interactableLayer; 

    private void Start()
    {
        // Initialize the inventory
        if (playerInventory != null)
        {
            playerInventory.Initialize(); // Initialize the inventory when the game starts
            Debug.Log("Inventory Initialized!");
        }
        else
        {
            Debug.LogWarning("Chưa gán PlayerInventory vào PlayerInteractor!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Check phím E
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        // Quét các collider trong bán kính interactRadius quanh vị trí interactPoint
        Collider2D collider = Physics2D.OverlapCircle(interactPoint.position, interactRadius, interactableLayer); 

        if (collider != null) 
        {
            IInteractable interactableObject = collider.GetComponent<IInteractable>(); 
            if (interactableObject != null) 
            {
                interactableObject.Interact(); 
            }
            else
            {
                Debug.Log($"Vật thể {collider.name} nằm trong Layer nhưng không có component IInteractable!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactPoint == null) return; 
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
    }
}