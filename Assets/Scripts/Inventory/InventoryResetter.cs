using UnityEngine;
using Inventory.Model;

public class InventoryResetter : MonoBehaviour
{
    [Header("Data to Reset")]
    [SerializeField] 
    private InventorySO inventoryData;

    // Hàm Awake chạy đầu tiên, trước cả hàm Start() của InventoryController
    private void Awake()
    {
        if (inventoryData != null)
        {
            inventoryData.ClearInventory();
            Debug.Log("Inventory has been clear !");
        }
        else
        {
            Debug.LogWarning("inventory data has not yet been assigned !");
        }
    }
}
