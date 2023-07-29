using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}
[CreateAssetMenu(fileName = "Assets/ScriptableObjects/Items/New Item Data", menuName = "2D_RPG/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    [Range(0, 100)] public float dropChance;
}
