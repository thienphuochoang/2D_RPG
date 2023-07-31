using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : PersistentObject<InventoryManager>
{
    [SerializeField]
    private List<InventoryItem> startingItems = new List<InventoryItem>();
    [SerializeField]
    private List<InventoryItem> equipmentItems;
    public Dictionary<ItemData_Equipment, InventoryItem> equipment;
    [SerializeField]
    private List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventory;
    [SerializeField]
    private List<InventoryItem> stashItems;
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
    
    [Header("Stat UI")]
    [SerializeField]
    private Transform _statSlotParent;
    
    [Header("Flask Cooldown")]
    private float lastTimeUsedHPFlask;
    private float lastTimeUsedManaFlask;
    private float hpFlaskCooldown;
    private float manaFlaskCooldown;
    

    private ItemSlot_UI[] _inventoryItemSlots;
    private ItemSlot_UI[] _stashItemSlots;
    private EquipmentSlot_UI[] _equipmentItemSlots;
    private StatSlot_UI[] _statSlots;
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
        
        _statSlots = _statSlotParent.GetComponentsInChildren<StatSlot_UI>();
        TransferStartingItemsToEquipmentItems();
    }

    private void TransferStartingItemsToEquipmentItems()
    {
        if (startingItems.Count > 0)
        {
            foreach (var startingItem in startingItems)
            {
                EquipItem(startingItem.itemData);
            }
        }
        startingItems.Clear();
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
        
        for (int i = 0; i < _statSlots.Length; i++)
        {
            _statSlots[i].UpdateStatValueUI();
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

    public List<InventoryItem> GetEquipmentList() => equipmentItems;
    public List<InventoryItem> GetStashList() => stashItems;
    public List<InventoryItem> GetInventoryList() => inventoryItems;

    public ItemData_Equipment GetEquipment(EquipmentType type)
    {
        ItemData_Equipment equipedItem = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipment)
        {
            if (item.Key.equipmentType == type)
                equipedItem = item.Key;
        }
        return equipedItem;
    }

    public void UseFlask(EquipmentType flaskType)
    {
        switch (flaskType)
        {
            case EquipmentType.HpFlask:
            {
                ItemData_Equipment currentFlask = GetEquipment(EquipmentType.HpFlask);
                if (currentFlask == null)
                    return;
                bool canUseFlask = Time.time > lastTimeUsedHPFlask + hpFlaskCooldown;
                if (canUseFlask)
                {
                    hpFlaskCooldown = currentFlask.itemCooldown;
                    currentFlask.ExecuteItemEffect();
                    lastTimeUsedHPFlask = Time.time;
                }
                else
                {
                    Debug.Log("Flask is on cooldown");
                }
                break;
            }
            case EquipmentType.ManaFlask:
            {
                ItemData_Equipment currentFlask = GetEquipment(EquipmentType.ManaFlask);
                if (currentFlask == null)
                    return;
                bool canUseFlask = Time.time > lastTimeUsedManaFlask + manaFlaskCooldown;
                if (canUseFlask)
                {
                    manaFlaskCooldown = currentFlask.itemCooldown;
                    currentFlask.ExecuteItemEffect();
                    lastTimeUsedManaFlask = Time.time;
                }
                else
                {
                    Debug.Log("Flask is on cooldown");
                }
                break;
            }
        }
    }
}
