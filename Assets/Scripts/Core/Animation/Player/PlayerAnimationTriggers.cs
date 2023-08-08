using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player _thePlayer => GetComponentInParent<Player>();

    private void TriggerAnimation()
    {
        _thePlayer.TriggerAnimation();
    }

    private void TriggerCounterFX()
    {
        _thePlayer.GetComponent<EntityEffect>().GenerateCounterFX(_thePlayer.counterAttackPoint);
    }
    
    private void TriggerFireBulletProjectile()
    {
        if (SkillManager.Instance.fireBulletSkill.CanUseSkill())
            SkillManager.Instance.fireBulletSkill.UseSkill();
    }

    private void TriggerExplosionHole()
    {
        if (SkillManager.Instance.explosionHoleSkill.CanUseSkill())
            SkillManager.Instance.explosionHoleSkill.UseSkill();
    }

    private void TriggerPhysicalAttack()
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(_thePlayer.attackCheck.position, _thePlayer.attackCheckRadius);
        foreach (Collider2D hitObj in colliders)
        {
            if (hitObj.GetComponent<Enemy>() != null)
            {
                EnemyStats target = hitObj.GetComponent<EnemyStats>();
                _thePlayer.stats.DoDamage(target);
            }
        }
    }
}
