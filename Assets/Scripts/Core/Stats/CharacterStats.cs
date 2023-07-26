using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strength;
    public Stat damage;
    public Stat maxHealth;
    [SerializeField]
    private int _currentHealth;

    protected virtual void Start()
    {
        _currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamge(CharacterStats targetStats)
    {
        int totalDamage = damage.GetValue() + strength.GetValue();
        targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int inputDamage)
    {
        _currentHealth -= inputDamage;
        Debug.Log(inputDamage);
        if (_currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        
    }
}
