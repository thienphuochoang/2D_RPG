using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot_UI : ItemSlot_UI
{
    [SerializeField]
    private TextMeshProUGUI _craftItemName;

    private int defaultFonSize = 25;
    protected override void Start()
    {
        base.Start();
    }

    public void SetupCraftSlot(ItemData_Equipment itemDataEquipment)
    {
        if (itemDataEquipment == null) return;
        item.itemData = itemDataEquipment;
        itemImage.sprite = itemDataEquipment.itemIcon;
        _craftItemName.text = itemDataEquipment.itemName;
        if (_craftItemName.text.Length > 13)
        {
            _craftItemName.fontSize = _craftItemName.fontSize * 0.7f;
        }
        else
        {
            _craftItemName.fontSize = defaultFonSize;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        /*ItemData_Equipment craftItemDataEquipment = item.itemData as ItemData_Equipment;
        InventoryManager.Instance.CanCraft(craftItemDataEquipment, craftItemDataEquipment.craftingMaterials);*/
        _mainUI.craftWindowUI.SetupCraftWindow(item.itemData as ItemData_Equipment);
    }
}
