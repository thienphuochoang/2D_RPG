using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot_UI : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAmount;

    public InventoryItem item;
    
    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;
        if (item != null)
        {
            itemImage.GetComponent<Image>().enabled = true;
            itemImage.sprite = item.itemData.itemIcon;
            if (item.stackSize > 1)
            {
                itemAmount.text = item.stackSize.ToString();
            }
            else
            {
                itemAmount.text = "";
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;
        itemImage.GetComponent<Image>().enabled = false;
        itemImage.sprite = null;
        itemAmount.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item.itemData != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                InventoryManager.Instance.RemoveItem(item.itemData);
            }
            if (item.itemData.itemType == ItemType.Equipment)
            {
                InventoryManager.Instance.EquipItem(item.itemData);
            }
        }

    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Test OnDrag");
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Test OnDrop");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
