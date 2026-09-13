using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage; //using UnityEngine.UI;
    [SerializeField]
    private TMP_Text itemName;
    [SerializeField]
    private TMP_Text itemDescription;

    public void Awake()
    {
        ResetDescription();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDescription();
    }
    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemName.text = "";
        this.itemDescription.text = "";
    }

    public void SetDescription(Sprite sprite, string itemName, string itenmDescription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemName.text = itemName;
        this.itemDescription.text = itenmDescription;
    }
}


