using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private static readonly int SuccessfulCounterAttack = Animator.StringToHash("SuccessfulCounterAttack");

    public PlayerCounterAttackState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        stateTimer = player.counterAttackDuration;
        player.animator.SetBool(SuccessfulCounterAttack, false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.ResetZeroVelocity();
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (Collider2D hitObj in colliders)
        {
            if (hitObj.GetComponent<ProjectileController>() != null)
            {
                hitObj.GetComponent<ProjectileController>().FlipProjectile();
                stateTimer = 10f;
                player.animator.SetBool(SuccessfulCounterAttack, true);
                //player.GetComponent<EntityEffect>().GenerateCounterFX(player.transform);
            }
            if (hitObj.GetComponent<Enemy>() != null)
            {
                Enemy enemy = hitObj.GetComponent<Enemy>();
                if (enemy.CanBeStunned())
                {
                    player.GetComponent<PlayerStats>().DoDamage(enemy.GetComponent<EnemyStats>());
                    stateTimer = 10f;
                    player.animator.SetBool(SuccessfulCounterAttack, true);
                    //player.GetComponent<EntityEffect>().GenerateCounterFX(player.transform);
                }
            }
        }

        if (stateTimer < 0 || animationTriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void EndState()
    {
        base.EndState();
    }
}
