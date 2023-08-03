using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}
[CreateAssetMenu(fileName = "Assets/ScriptableObjects/Items/New Item Data", menuName = "2D_RPG/Item")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    [Range(0, 100)] public float dropChance;
    protected StringBuilder stringBuilder = new StringBuilder();

    private void OnValidate()
    {
    #if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(path);
    #endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
