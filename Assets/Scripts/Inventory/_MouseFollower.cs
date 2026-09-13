using System;
using UnityEngine;

public class _MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private UIInventoryItem item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite)
    {
        if (item != null)
        item.SetData(sprite);
    }

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        ((RectTransform)canvas.transform,
        Input.mousePosition,
        canvas.worldCamera,
        out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item Toggled {val}");
        gameObject.SetActive(val);
    }
}
