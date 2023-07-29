using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private ItemData _itemData;

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = "Item Object: " + _itemData.itemName;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = _itemData.itemName;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            InventoryManager.Instance.AddItem(_itemData);
            Destroy(this.gameObject);
        }
    }
}
