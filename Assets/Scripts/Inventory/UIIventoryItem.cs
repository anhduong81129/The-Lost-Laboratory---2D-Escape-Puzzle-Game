using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler 
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked, OnRightMouseBtnClick,
    OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;
    public bool IsEmpty => empty;

    public void Awake()
    {
        Deselect();
        ResetData();
    }

    public void Select()
    {
        if (borderImage != null)
        {
            borderImage.enabled = true;
        }
    }

    public void Deselect()
    {
        if (borderImage != null)
        {
            borderImage.enabled = false;
        }
    }

    public void SetData(Sprite sprite)
    {
        if (itemImage != null)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.empty = false;
        }
    }

    public void ResetData()
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(false);
            itemImage.sprite = null;
        }
        this.empty = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }


    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
        else if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
    }
}
