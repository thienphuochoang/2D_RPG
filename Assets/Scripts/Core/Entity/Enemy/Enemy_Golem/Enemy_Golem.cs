using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Golem : Enemy
{
    [Header("Ranged Attack Info")]
    public float rangedAttackDistance;
    public float laserAttackDistance;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileShootingTransform;
    private Player _player;
    public EnemyGolemIdleState idleState { get; private set; }
    public EnemyGolemMoveState moveState { get; private set; }
    public EnemyGolemBattleState battleState { get; private set; }
    public EnemyGolemMeleeAttackState meeleAttackState { get; private set; }
    public EnemyGolemRangedAttackState rangedAttackState { get; private set; }
    public EnemyGolemLaserAttackState laserAttackState { get; private set; }
    public EnemyGolemDeadState deadState { get; private set; }
    public EnemyGolemBlockState blockState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyGolemIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyGolemMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyGolemBattleState(this, stateMachine, "Idle", this);
        meeleAttackState = new EnemyGolemMeleeAttackState(this, stateMachine, "MeleeAttack", this);
        rangedAttackState = new EnemyGolemRangedAttackState(this, stateMachine, "RangedAttack", this);
        laserAttackState = new EnemyGolemLaserAttackState(this, stateMachine, "LaserAttack", this);
        deadState = new EnemyGolemDeadState(this, stateMachine, "Die", this);
        blockState = new EnemyGolemBlockState(this, stateMachine, "Block", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        _player = PlayerManager.Instance.player;
    }
    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    
    public void TriggerRangeAttackAnimation()
    {
        GameObject newProjectile = Instantiate(_projectilePrefab, _projectileShootingTransform.position, Quaternion.identity);
        Vector2 directionToPlayer = _player.transform.position - transform.position;
        //newProjectile.GetComponent<ProjectileController>().SetupProjectile(_projectileSpeed * facingDirection, stats);
        newProjectile.GetComponent<ArmProjectileController>().SetupProjectile(directionToPlayer, stats);
    }
}
