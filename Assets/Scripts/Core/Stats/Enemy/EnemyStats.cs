using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy _enemy;
    protected override void Start()
    {
        base.Start();
        _enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int inputDamage)
    {
        base.TakeDamage(inputDamage);
        _enemy.TriggerDamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        _enemy.Die();
    }
}