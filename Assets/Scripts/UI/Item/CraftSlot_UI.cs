using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot_UI : ItemSlot_UI
{
    private void OnEnable()
    {
        UpdateSlot(item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemData_Equipment craftItemDataEquipment = item.itemData as ItemData_Equipment;
        InventoryManager.Instance.CanCraft(craftItemDataEquipment, craftItemDataEquipment.craftingMaterials);
    }
}
