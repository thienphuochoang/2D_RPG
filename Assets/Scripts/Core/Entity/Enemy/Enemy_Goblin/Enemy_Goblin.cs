using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin : Enemy
{
    private Player _player;
    [Header("Ranged Attack Info")]
    public float rangedAttackDistance;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform _bombThrowingTransform;
    public EnemyGoblinIdleState idleState { get; private set; }
    public EnemyGoblinMoveState moveState { get; private set; }
    public EnemyGoblinBattleState battleState { get; private set; }
    public EnemyGoblinAttackState attackState { get; private set; }
    public EnemyGoblinAttack02State attack02State { get; private set; }
    public EnemyGoblinAttack03State attack03State { get; private set; }
    public EnemyGoblinStunnedState stunnedState { get; private set; }
    public EnemyGoblinDeadState deadState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyGoblinIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyGoblinMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyGoblinBattleState(this, stateMachine, "Move", this);
        attackState = new EnemyGoblinAttackState(this, stateMachine, "Attack", this);
        attack02State = new EnemyGoblinAttack02State(this, stateMachine, "Attack02", this);
        attack03State = new EnemyGoblinAttack03State(this, stateMachine, "Attack03", this);
        stunnedState = new EnemyGoblinStunnedState(this, stateMachine, "Stunned", this);
        deadState = new EnemyGoblinDeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        _player = PlayerManager.Instance.player;
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
    public void TriggerRangeAttackAnimation()
    {
        GameObject newBomb = Instantiate(_bombPrefab, _bombThrowingTransform.position, Quaternion.identity);
        //Vector2 directionToPlayer = _player.transform.position - transform.position;
        //newProjectile.GetComponent<ProjectileController>().SetupProjectile(_projectileSpeed * facingDirection, stats);
        newBomb.GetComponent<BombProjectileController>().SetupProjectile(_player.transform.position, stats);
    }
}
