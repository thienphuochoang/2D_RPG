using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot_UI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemBackgroundImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAmount;

    public InventoryItem item;
    
    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;
        if (item != null)
        {
            itemBackgroundImage.GetComponent<Image>().enabled = true;
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
        itemBackgroundImage.GetComponent<Image>().enabled = false;
        itemImage.GetComponent<Image>().enabled = false;
        itemImage.sprite = null;
        itemAmount.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item.itemData.itemType == ItemType.Equipment)
        {
            InventoryManager.Instance.EquipItem(item.itemData);
        }
    }
}
