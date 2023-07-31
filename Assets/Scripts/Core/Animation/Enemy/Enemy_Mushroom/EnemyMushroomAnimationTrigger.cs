using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomAnimationTrigger : MonoBehaviour
{
    private Enemy_Mushroom _enemy => GetComponentInParent<Enemy_Mushroom>();

    private void TriggerAnimation()
    {
        _enemy.TriggerAnimation();
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

    private void EnableCounterAttack() => _enemy.EnableCounterAttack();
    private void DisableCounterAttack() => _enemy.DisableCounterAttack();
}
