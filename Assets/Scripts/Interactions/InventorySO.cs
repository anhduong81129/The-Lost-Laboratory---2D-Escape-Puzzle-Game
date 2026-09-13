using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            if (inventoryItems == null)
            {
                inventoryItems = new List<InventoryItem>();
            }

            if (inventoryItems.Count == Size)
            {
                return;
            }

            inventoryItems.Clear();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public void ClearInventory()
        {
            if (inventoryItems == null)
            {
                inventoryItems = new List<InventoryItem>();
            }

            inventoryItems.Clear();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
            
            InformAboutChange();
        }

        // add anm item to inventory
        public bool AddItem(ItemSO item)
        {
            if (item == null) return false;

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem { item = item };
                    InformAboutChange();
                    return true; // add successful
                }
            }

            Debug.Log("Inventory Full!");
            return false; // inventory is full
        }

        // delete item at a specific index in the inventory
        public void RemoveItem(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < inventoryItems.Count)
            {
                if (inventoryItems[itemIndex].IsEmpty) return;

                inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                InformAboutChange();
            }
        }

        // check if the inventory is full
        public bool IsInventoryFull()
            => inventoryItems.Any(item => item.IsEmpty) == false;

        // get current state of the inventory as a dictionary
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < inventoryItems.Count)
                return inventoryItems[itemIndex];

            return InventoryItem.GetEmptyItem();
        }

        // swap position of two items in the inventory
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            if (itemIndex_1 < 0 || itemIndex_1 >= inventoryItems.Count ||
                itemIndex_2 < 0 || itemIndex_2 >= inventoryItems.Count) return;

            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public bool ContainsItem(ItemSO item) // check if the inventory contains a specific item
        {
            if (item == null) return false;
            foreach (var inventoryItem in inventoryItems)
            {
                if (!inventoryItem.IsEmpty && inventoryItem.item == item)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveItem(ItemSO item) // delete a specific item from the inventory
        {
            if (item == null) return;
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItems[i].IsEmpty && inventoryItems[i].item == item)
                {
                    inventoryItems[i] = InventoryItem.GetEmptyItem();
                    InformAboutChange(); // Cập nhật lại UI túi đồ
                    return;
                }
            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public ItemSO item;
        public bool IsEmpty => item == null;

        public static InventoryItem GetEmptyItem()
            => new InventoryItem { item = null };
    }
}