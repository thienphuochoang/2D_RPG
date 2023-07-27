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
    [SerializeField]
    private int _currentHealth;

    protected virtual void Start()
    {
        _currentHealth = maxHealth.GetValue() + vitality.GetValue() * 4;
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
        _currentHealth -= inputDamage;
        if (_currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        
    }
}
