using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemAnimationTrigger : MonoBehaviour
{
    private Enemy_Golem _enemy => GetComponentInParent<Enemy_Golem>();

    private void TriggerAnimation()
    {
        _enemy.TriggerAnimation();
    }
    
    private void TriggerRangeAttackAnimation()
    {
        _enemy.TriggerRangeAttackAnimation();
    }

    private void TriggerAttack()
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(_enemy.attackCheck.position, _enemy.attackCheckRadius);
        foreach (Collider2D hitObj in colliders)
        {
            if (hitObj.GetComponent<Player>() != null)
            {
                PlayerStats target = hitObj.GetComponent<PlayerStats>();
                target.GetComponent<EntityEffect>().ShakeScreen();
                _enemy.stats.DoDamage(target);
            }
        }
    }
}
