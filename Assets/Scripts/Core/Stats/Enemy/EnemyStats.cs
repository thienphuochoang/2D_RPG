using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy _enemy;
    private ItemDrop _dropItem;
    protected override void Start()
    {
        base.Start();
        _enemy = GetComponent<Enemy>();
        _dropItem = GetComponent<ItemDrop>();
        _dropItem.GenerateDropList();
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
        _dropItem.DropItem();
    }
}
