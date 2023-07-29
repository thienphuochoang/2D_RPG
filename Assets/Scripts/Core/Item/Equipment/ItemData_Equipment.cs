using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Head,
    Body,
    Hands,
    Shoes,
    Amulet,
    Potion
}
[CreateAssetMenu(fileName = "Assets/ScriptableObjects/Items/New Equipment Data", menuName = "2D_RPG/Item_Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
}
