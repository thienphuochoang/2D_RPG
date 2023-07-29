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
    [Header("Major Stats")]
    public int strength; // Increase physical damage by 1
    public int agility; // Increase evasion by 1%
    public int intelligence; // Increase magic damage by 1 and maximum mana by 4
    public int vitality; // Increase health by 4;
    
    [Header("Sub-stats")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int damage;

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;
    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);
        
        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.damage.AddModifier(damage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);
        
        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.damage.RemoveModifier(damage);
    }
}
