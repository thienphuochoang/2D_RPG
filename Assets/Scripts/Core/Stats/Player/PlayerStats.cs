using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player _player;
    public Stat maxMana;
    public int currentMana { get; private set; }
    public event System.Action OnManaChanged;

    protected override void Start()
    {
        base.Start();
        _player = GetComponent<Player>();
        maxMana.SetDefaultValue(GetMaxManaValue());
        currentMana = maxMana.GetValue();
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
}
