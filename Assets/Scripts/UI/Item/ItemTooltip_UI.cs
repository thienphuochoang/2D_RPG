using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTooltip_UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemNameText;
    [SerializeField]
    private TextMeshProUGUI _itemTypeText;
    [SerializeField]
    private TextMeshProUGUI _itemDescriptionText;

    private int defaultFontSize = 18;
    public void ShowToolTip(ItemData item)
    {
        if (item == null) return;
        _itemNameText.text = item.itemName;
        if (item.itemType == ItemType.Equipment)
        {
            ItemData_Equipment itemDataEquipment = item as ItemData_Equipment;
            _itemTypeText.text = itemDataEquipment.equipmentType.ToString();
        }
        else
        {
            _itemTypeText.text = item.itemType.ToString();
        }

        if (_itemNameText.text.Length > 15)
        {
            _itemNameText.fontSize = _itemNameText.fontSize * 0.6f;
        }
        else
        {
            _itemNameText.fontSize = defaultFontSize;
        }

        _itemDescriptionText.text = item.GetDescription();
        
        
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        _itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }
    
}
