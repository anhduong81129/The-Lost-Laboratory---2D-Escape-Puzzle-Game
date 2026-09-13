using UnityEngine;
using System.Collections.Generic;
using Inventory.Model;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private InventorySO InventoryData;

    private void Start()
    {
        if (inventoryUI == null)
        {
            Debug.LogError("Inventory UI is not assigned in the InventoryController.");
            return;
        }

        if (InventoryData == null)
        {
            Debug.Log("Inventory Data is not assigned in Inventory Controller.");
            return;
        }

        InventoryData.Initialize();
        inventoryUI.InitializeInventoryUI(InventoryData.Size);
        inventoryUI.Hide();

        InventoryData.OnInventoryUpdated += UpdateInventoryUI;
        inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;

        UpdateInventoryUI(InventoryData.GetCurrentInventoryState());
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems(); // delete all items in the UI before updating with the new state

        // Update the UI with the current state of the inventory
        foreach (var item in inventoryState)
        {
            if (!item.Value.IsEmpty)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage);
            }
        }
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
        
        if (inventoryItem.IsEmpty) 
        {
        inventoryUI.ResetDescription(); 
        return;
        }

        ItemSO item = inventoryItem.item;

        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description);
        
        // Gọi hàm từ UIInventoryDescription (nếu có tham chiếu) 
        // Lưu ý: Vì UIInventoryPage đang giữ biến itemDescription, ta cần gọi gián tiếp.
        // Để đơn giản, UI của bạn tự động quản lý qua file khác, 
        // nhưng với cấu trúc này bạn có thể bổ sung update Description ở đây.
    }

    public void ToogleInventoryUI()
    {
        if (inventoryUI == null) return;
        
        if (inventoryUI.gameObject.activeSelf) // check if the inventory UI is not active and enabled
        {
            inventoryUI.Hide();
        }
        else
        {
            inventoryUI.Show();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToogleInventoryUI();
        }
    }
}
