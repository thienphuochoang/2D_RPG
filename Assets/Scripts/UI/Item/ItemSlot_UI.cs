using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot_UI : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemAmount;

    protected MainUI _mainUI;
    public InventoryItem item;

    protected virtual void Start()
    {
        _mainUI = GetComponentInParent<MainUI>();
    }

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
        if (item != null)
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
        _mainUI.itemTooltipUI.HideToolTip();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {

    }

    public virtual void OnDrop(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            //_mainUI.itemTooltipUI.GetComponent<RectTransform>().localPosition = new Vector3(10f, 10f, 0f);
            _mainUI.itemTooltipUI.transform.position = this.transform.position;
            _mainUI.itemTooltipUI.ShowToolTip(item.itemData);
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
            _mainUI.itemTooltipUI.HideToolTip();
    }
}
