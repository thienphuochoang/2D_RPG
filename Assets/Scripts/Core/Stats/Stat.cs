using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Strength,
    Agility,
    Intelligence,
    Vitality,
    PhysicalDamage,
    MagicalDamage,
    Health,
    Mana,
    Armor,
    Evasion
}
[System.Serializable]
public class Stat
{
    [SerializeField]
    private int _baseValue;
    public StatType statType;
    public List<int> modifiers;
    public int GetValue()
    {
        int finalValue = _baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void SetDefaultValue(int value)
    {
        _baseValue = value;
    }

    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        modifiers.Remove(modifier);
    }
}
