using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : PersistentObject<InventoryManager>
{
    public List<InventoryItem> equipmentItems;
    public Dictionary<ItemData_Equipment, InventoryItem> equipment;
    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventory;
    public List<InventoryItem> stashItems;
    public Dictionary<ItemData, InventoryItem> stash;

    [Header("Inventory UI")]
    [SerializeField]
    private Transform _inventorySlotParent;

    [Header("Stash UI")]
    [SerializeField]
    private Transform _stashSlotParent;
    
    [Header("Equipment UI")]
    [SerializeField]
    private Transform _equipmentSlotParent;

    private ItemSlot_UI[] _inventoryItemSlots;
    private ItemSlot_UI[] _stashItemSlots;
    private EquipmentSlot_UI[] _equipmentItemSlots;
    protected override void Start()
    {
        base.Start();
        
        equipmentItems = new List<InventoryItem>();
        equipment = new Dictionary<ItemData_Equipment, InventoryItem>();
        _equipmentItemSlots = _equipmentSlotParent.GetComponentsInChildren<EquipmentSlot_UI>();

        inventoryItems = new List<InventoryItem>();
        inventory = new Dictionary<ItemData, InventoryItem>();
        _inventoryItemSlots = _inventorySlotParent.GetComponentsInChildren<ItemSlot_UI>();
        
        stashItems = new List<InventoryItem>();
        stash = new Dictionary<ItemData, InventoryItem>();
        _stashItemSlots = _stashSlotParent.GetComponentsInChildren<ItemSlot_UI>();
    }

    public void EquipItem(ItemData itemData)
    {
        ItemData_Equipment newEquipment = itemData as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldEquipment = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipment)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                oldEquipment = item.Key;
            }
        }

        if (oldEquipment != null)
        {
            int indexOfOldEquipment = UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }
            
        
        equipmentItems.Add(newItem);
        equipment.Add(newEquipment, newItem);
        newEquipment.AddModifiers();
        RemoveItem(itemData);
        UpdateSlotUI();
    }

    public int UnequipItem(ItemData_Equipment itemToRemove)
    {
        if (equipment.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipmentItems.Remove(value);
            equipment.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
        return equipmentItems.IndexOf(value);
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < _equipmentItemSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipment)
            {
                if (item.Key.equipmentType == _equipmentItemSlots[i].equipmentType)
                {
                    _equipmentItemSlots[i].UpdateSlot(item.Value);
                }
            }
        }
        for (int i = 0; i < _inventoryItemSlots.Length; i++)
        {
            _inventoryItemSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < _stashItemSlots.Length; i++)
        {
            _stashItemSlots[i].CleanUpSlot();
        }
        
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            _inventoryItemSlots[i].UpdateSlot(inventoryItems[i]);
        }

        for (int i = 0; i < stashItems.Count; i++)
        {
            _stashItemSlots[i].UpdateSlot(stashItems[i]);
        }
    }
    public void AddItem(ItemData itemData)
    {
        switch (itemData.itemType)
        {
            case ItemType.Equipment:
            {
                AddToInventory(itemData);
                break;
            }
            case ItemType.Material:
            {
                AddToStash(itemData);
                break;
            }
        }
        UpdateSlotUI();
    }

    private void AddToStash(ItemData itemData)
    {
        if (stash.TryGetValue(itemData, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            stashItems.Add(newItem);
            stash.Add(itemData, newItem);
            //_stashItemSlots.Add(Instantiate(_stashItemSlotPrefab, _stashSlotParent));
        }
    }

    private void AddToInventory(ItemData itemData)
    {
        if (inventory.TryGetValue(itemData, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventoryItems.Add(newItem);
            inventory.Add(itemData, newItem);
            //_inventoryItemSlots.Add(Instantiate(_inventoryItemSlotPrefab, _inventorySlotParent));
            //Debug.Log("Test");
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        if (inventory.TryGetValue(itemData, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventory.Remove(itemData);
            }
            else
            {
                value.RemoveStack();
            }
        }
        
        if (stash.TryGetValue(itemData, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stashItems.Remove(stashValue);
                stash.Remove(itemData);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

    public bool CanCraft(ItemData_Equipment itemNeedToBeCrafted, List<InventoryItem> requiredItems)
    {
        List<InventoryItem> materialsToBeRemoved = new List<InventoryItem>();
        {
            for (int i = 0; i < requiredItems.Count; i++)
            {
                if (stash.TryGetValue(requiredItems[i].itemData, out InventoryItem stashValue))
                {
                    if (stashValue.stackSize < requiredItems[i].stackSize)
                    {
                        Debug.Log("Not enough materials");
                        return false;
                    }
                    else
                    {
                        materialsToBeRemoved.Add(stashValue);
                    }
                }
                else
                {
                    Debug.Log("Not enough materials");
                    return false;
                }
            }
        }
        for (int i = 0; i < materialsToBeRemoved.Count; i++)
        {
            RemoveItem(materialsToBeRemoved[i].itemData);
        }
        AddItem(itemNeedToBeCrafted);
        Debug.Log("Craft successfully: " + itemNeedToBeCrafted.itemName);
        return true;
    }
}
