using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIInventoryDescription itemDescription;

    [SerializeField]
    private _MouseFollower mouseFollower;

    private UIInventoryItem selectedItem; 
    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private int currentlyDraggedItemIndex = -1; 

    public event Action<int> OnDescriptionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        mouseFollower.Toggle(false);
        if (itemDescription != null)
        {
            itemDescription.ResetDescription();
        }
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        foreach (var item in listOfUIItems) // clear the existing items in the list and destroy their game objects
        {
            if (item != null)
            {
                item.OnItemClicked -= HandleItemClicked;
                Destroy(item.gameObject);
            }
        }
        listOfUIItems.Clear();
        DeselectAllItems();

        for (int i = 0; i < inventorySize; i++) // create new UIInventoryItem instances based on the specified inventory size
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, contentPanel, false);
            uiItem.OnItemClicked += HandleItemClicked;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick+= HandleShowItemAction;
            listOfUIItems.Add(uiItem);
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage);
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
        DeselectAllItems();

        if (itemDescription != null)
        {
            itemDescription.ResetDescription();
        }
    }

    public void Hide()
    {
        DeselectAllItems();

        if (itemDescription != null)
        {
            itemDescription.ResetDescription();
        }

        gameObject.SetActive(false);
        ResetDragItem();
    }

    private void HandleItemClicked(UIInventoryItem item)
    {
        if (selectedItem == item) // if the item is clicked -> deselect it and reset the description
        {
            DeselectAllItems();
            itemDescription.ResetDescription();
            return;
        }

        if (selectedItem != null) // deselect the previously selected item
        {
            selectedItem.Deselect();
        }

        selectedItem = item;  // select new item and update the description
        selectedItem.Select();

        HandleItemSelection(item);
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
    }


    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void ResetDragItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

     public void CreateDragItem(Sprite sprite)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite);
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDragItem();
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
    }
    private void HandleShowItemAction(UIInventoryItem inventoryItemUI)
    {

    }

    public void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
    }

    public void ResetDescription()
    {
        if (itemDescription != null)
        {
            itemDescription.ResetDescription();
        }
    }

    public void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        // Kiểm tra xem biến quản lý panel description có tồn tại không
        // (Lưu ý: kiểm tra tên biến itemDescription trong UIInventoryPage của bạn cho đúng)
        if (itemDescription != null)
        {
            itemDescription.SetDescription(itemImage, name, description);
            itemDescription.Show(); // Hoặc Bật Panel Description lên
        }
    }
}
