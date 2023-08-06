using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeAnimationTrigger : MonoBehaviour
{
    private Enemy_FlyingEye _enemy => GetComponentInParent<Enemy_FlyingEye>();

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
                _enemy.stats.DoDamage(target);
            }
        }
    }
}
