using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public PlayerStateMachine stateMachine { get; private set; } 
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirAttackState airAttackState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerFireBulletState fireBulletState { get; private set; }
    public PlayerExplosionHoleState explosionHoleState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    public bool isBusy { get; private set; }
    public CapsuleCollider2D col { get; private set; }
    public Skill whatSkillIsUsing;

    [Header("Move info")]
    public float moveSpeed = 7f;
    public float jumpForce = 15f;

    [Header("Dash info")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;
    [HideInInspector]
    public float dashDirection;


    [Header("Attack info")]
    public Vector2[] attackOffsetMovement;

    [Header("Counter attack info")]
    public float counterAttackDuration;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        airAttackState = new PlayerAirAttackState(this, stateMachine, "AirAttack");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        fireBulletState = new PlayerFireBulletState(this, stateMachine, "FireBullet");
        explosionHoleState = new PlayerExplosionHoleState(this, stateMachine, "ExplosionHoleActivate");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }
    
    protected override void Start()
    {
        base.Start();
        col = GetComponent<CapsuleCollider2D>();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.UpdateState();
        CheckForDashInput();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InventoryManager.Instance.UseFlask(EquipmentType.HpFlask);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            InventoryManager.Instance.UseFlask(EquipmentType.ManaFlask);
        }
    }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    public void TriggerAnimation() => stateMachine.currentState.FinishAnimationTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.S) && IsGroundedDetected() && SkillManager.Instance.dash.CanUseSkill())
        {
            SkillManager.Instance.dash.UseSkill();
            whatSkillIsUsing = SkillManager.Instance.dash;
            dashDirection = Input.GetAxisRaw("Horizontal");
            if (dashDirection == 0)
                dashDirection = facingDirection;
            stateMachine.ChangeState(dashState);
        }
    }
}
