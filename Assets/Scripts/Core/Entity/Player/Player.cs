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
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public bool isBusy { get; private set; }

    [Header("Move info")]
    public float moveSpeed = 7f;
    public float jumpForce = 15f;
    

    [Header("Attack info")]
    public Vector2[] attackOffsetMovement;

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
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.UpdateState();
    }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    public void TriggerAnimation() => stateMachine.currentState.FinishAnimationTrigger();

    /*protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.UpdateState();
        Move();
        CheckInput();

        _comboTimeCounter -= Time.deltaTime;
        
        ControlFlip();
        ControlAnimator();
    }
    
    private void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
    }

    private void Move()
    {
        if (_isAttacking)
            Rigidbody2D.velocity = new Vector2(0,0);
        else
            Rigidbody2D.velocity = new Vector2(_xInput * moveSpeed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        if (IsGrounded)
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
    }

    private void StartAttackEvent()
    {
        if (IsGrounded == false)
            return;
        if (_comboTimeCounter < 0)
            _comboCounter = 0;
        _isAttacking = true;
        _comboTimeCounter = comboTime;
    }

    private void ControlAnimator()
    {
        bool isMoving = Rigidbody2D.velocity.x != 0;
        Animator.SetFloat(yVelocityParameter, Rigidbody2D.velocity.y);
        Animator.SetBool(IsMovingParameter, isMoving);
        Animator.SetBool(IsGroundedParameter, IsGrounded);
        Animator.SetBool(IsAttackingParameter, _isAttacking);
        Animator.SetInteger(ComboCounterParameter, _comboCounter);
    }



    public void AttackOver()
    {
        _isAttacking = false;
        _comboCounter++;
        if (_comboCounter >= 2)
            _comboCounter = 0;
    }

    private void ControlFlip()
    {
        if (Rigidbody2D.velocity.x > 0 && !FacingRight)
        {
            Flip();
        }
        else if (Rigidbody2D.velocity.x < 0 && FacingRight)
        {
            Flip();
        }
    }*/
}
