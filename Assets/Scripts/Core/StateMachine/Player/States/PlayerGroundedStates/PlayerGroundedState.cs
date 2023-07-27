using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKey(KeyCode.A))
            stateMachine.ChangeState(player.primaryAttackState);
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundedDetected())
            stateMachine.ChangeState(player.jumpState);
        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterAttackState);
        if (Input.GetKeyDown(KeyCode.Alpha1) && player.IsGroundedDetected() &&
            SkillManager.Instance.fireBulletSkill.CanUseSkill())
        {
            player.whatSkillIsUsing = SkillManager.Instance.fireBulletSkill;
            stateMachine.ChangeState(player.fireBulletState);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && player.IsGroundedDetected() &&
            SkillManager.Instance.explosionHoleSkill.CanUseSkill())
        {
            player.whatSkillIsUsing = SkillManager.Instance.explosionHoleSkill;
            stateMachine.ChangeState(player.explosionHoleState);
        }
    }

    public override void EndState()
    {
        base.EndState();
    }
}
