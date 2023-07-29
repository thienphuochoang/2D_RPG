using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player _player;
    public Stat maxMana;
    public int currentMana { get; private set; }

    protected override void Start()
    {
        base.Start();
        _player = GetComponent<Player>();
        maxMana.SetDefaultValue(intelligence.GetValue() * 4);
        currentMana = maxMana.GetValue();
    }
    
    public virtual void DoMagicalDamage(CharacterStats targetStats, Skill skill)
    {
        int totalMagicalDamage = intelligence.GetValue() + skill.skillBaseDamage;
        Debug.Log(totalMagicalDamage);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        targetStats.TakeDamage(totalMagicalDamage);
    }

    public void ConsumeMana(int manaCost)
    {
        currentMana -= manaCost;
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
