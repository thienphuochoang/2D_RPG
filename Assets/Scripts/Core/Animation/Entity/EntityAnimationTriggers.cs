using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Player _thePlayer => PlayerManager.Instance.player;
    
    private void TriggerMagicalAttack()
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(_thePlayer.attackCheck.position, _thePlayer.attackCheckRadius);
        foreach (Collider2D hitObj in colliders)
        {
            if (hitObj.GetComponent<Enemy>() != null)
            {
                EnemyStats target = hitObj.GetComponent<EnemyStats>();
                PlayerStats playerStats = _thePlayer.GetComponent<PlayerStats>();
                playerStats.DoMagicalDamage(target, _thePlayer.whatSkillIsUsing);
            }
        }
    }
}
