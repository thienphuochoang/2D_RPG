using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    public EnemyMushroomIdleState idleState { get; private set; }
    public EnemyMushroomMoveState moveState { get; private set; }
    public EnemyMushroomBattleState battleState { get; private set; }
    public EnemyMushroomAttackState attackState { get; private set; }
    public EnemyMushroomAttack02State attack02State { get; private set; }
    public EnemyMushroomAttack03State attack03State { get; private set; }
    public EnemyMushroomStunnedState stunnedState { get; private set; }
    public EnemyMushroomDeadState deadState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyMushroomIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyMushroomMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyMushroomBattleState(this, stateMachine, "Move", this);
        attackState = new EnemyMushroomAttackState(this, stateMachine, "Attack", this);
        attack02State = new EnemyMushroomAttack02State(this, stateMachine, "Attack02", this);
        attack03State = new EnemyMushroomAttack03State(this, stateMachine, "Attack03", this);
        stunnedState = new EnemyMushroomStunnedState(this, stateMachine, "Stunned", this);
        deadState = new EnemyMushroomDeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
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

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
