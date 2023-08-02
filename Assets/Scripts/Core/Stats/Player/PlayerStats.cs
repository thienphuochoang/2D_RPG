using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player _player;
    public Stat mana;
    public int currentMana { get; private set; }
    public event System.Action OnManaChanged;

    protected override void Start()
    {
        base.Start();
        _player = GetComponent<Player>();
        mana.SetDefaultValue(mana.GetValue() + GetMaxManaValue());
        currentMana = mana.GetValue();
    }
    
    public virtual void DoMagicalDamage(CharacterStats targetStats, Skill skill)
    {
        int totalMagicalDamage = intelligence.GetValue() + skill.skillBaseDamage;
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        targetStats.TakeDamage(totalMagicalDamage);
    }

    public void ConsumeMana(int manaCost)
    {
        currentMana -= manaCost;
        if (currentMana <= 0)
            currentMana = 0;
        OnManaChanged?.Invoke();
    }
    
    public void IncreaseManaBy(int manaAmount)
    {
        currentMana += manaAmount;
        if (currentMana > GetMaxManaValue())
            currentMana = GetMaxManaValue();
        OnManaChanged?.Invoke();
    }
    
    public int GetMaxManaValue()
    {
        return intelligence.GetValue() * 4;
    }
    
    public override void TakeDamage(int inputDamage)
    {
        base.TakeDamage(inputDamage);
        _player.TriggerDamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        _player.Die();
    }

    public Stat GetStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.Strength:
                return strength;
            case StatType.Agility:
                return agility;
            case StatType.Intelligence:
                return intelligence;
            case StatType.Vitality:
                return vitality;
            case StatType.PhysicalDamage:
                return damage;
            case StatType.MagicalDamage:
                return intelligence;
            case StatType.Armor:
                return armor;
            case StatType.Evasion:
                return evasion;
            case StatType.Health:
                return health;
            case StatType.Mana:
                return mana;
        }

        return null;
    }
}
