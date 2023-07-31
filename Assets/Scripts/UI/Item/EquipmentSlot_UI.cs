using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot_UI : ItemSlot_UI
{
    public EquipmentType equipmentType;

    private void OnValidate()
    {
        gameObject.name = "Equipment slot: " + equipmentType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.itemData == null) return;
        InventoryManager.Instance.UnequipItem(item.itemData as ItemData_Equipment);
        InventoryManager.Instance.AddItem(item.itemData as ItemData_Equipment);
        CleanUpSlot();
    }
}
