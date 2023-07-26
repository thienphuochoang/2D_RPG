using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    public EnemyMushroomIdleState idleState { get; private set; }
    public EnemyMushroomMoveState moveState { get; private set; }
    public EnemyMushroomBattleState battleState { get; private set; }
    public EnemyMushroomAttackState attackState { get; private set; }
    public EnemyMushroomStunnedState stunnedState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyMushroomIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyMushroomMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyMushroomBattleState(this, stateMachine, "Move", this);
        attackState = new EnemyMushroomAttackState(this, stateMachine, "Attack", this);
        stunnedState = new EnemyMushroomStunnedState(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(stunnedState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }
}
