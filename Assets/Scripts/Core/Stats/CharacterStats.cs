using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major Stats")]
    public Stat strength; // Increase physical damage by 1
    public Stat agility; // Increase evasion by 1%
    public Stat intelligence; // Increase magic damage by 1 and maximum mana by 4
    public Stat vitality; // Increase health by 4;
    
    [Header("Sub-stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat damage;
    public int currentHealth { get; private set; }
    public event System.Action OnHealthChanged;

    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
    }

    public virtual void DoDamge(CharacterStats targetStats)
    {
        if (TargetCanAvoidAttack(targetStats))
            return;
        int totalDamage = damage.GetValue() + strength.GetValue();
        totalDamage = CheckTargetArmor(targetStats, totalDamage);
        targetStats.TakeDamage(totalDamage);
    }

    private int CheckTargetArmor(CharacterStats targetStats, int totalDamage)
    {
        totalDamage -= targetStats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats targetStats)
    {
        int totalEvasion = targetStats.evasion.GetValue() + targetStats.agility.GetValue();
        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }
    

    public virtual void TakeDamage(int inputDamage)
    {
        DecreaseHealthBy(inputDamage);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    protected virtual void DecreaseHealthBy(int inputDamage)
    {
        currentHealth -= inputDamage;
        OnHealthChanged?.Invoke();
    }

    protected virtual void Die()
    {
        
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 4;
    }
}
