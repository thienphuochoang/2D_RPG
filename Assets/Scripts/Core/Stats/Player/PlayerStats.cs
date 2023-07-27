using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player _player;
    protected override void Start()
    {
        base.Start();
        _player = GetComponent<Player>();
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
