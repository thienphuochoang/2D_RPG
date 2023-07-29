using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private ItemData _itemData;
    private Rigidbody2D _rb;

    
    /*private void OnValidate()
    {
        if (_itemData == null) return;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = "Item Object: " + _itemData.itemName;
    }*/
    private void SetupVisual()
    {
        if (_itemData == null) return;
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = "Item Object: " + _itemData.itemName;
    }

    public void SetupItem(ItemData itemData, Vector2 velocity)
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _itemData = itemData;
        _rb.velocity = velocity;
        SetupVisual();
    }

    public void PickUpItem()
    {
        InventoryManager.Instance.AddItem(_itemData);
        Destroy(this.gameObject);
    }
}
