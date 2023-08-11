using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlyingEye : Enemy
{
    [Header("Enemy Flying Eye Projectile Info")]
    [SerializeField]
    private GameObject _projectilePrefab;
    [Header("Enemy Flying Eye Player Detection View")]
    public float viewRadius = 10f;       // Radius of the field of view
    public float viewAngle = 90f;        // Half of the angle of the field of view

    [Header("Enemy Flying Eye Movement Info")]
    public float maxHeightDistance = 2f;
    public Vector3 initialPosition { get; private set; }
    public float currentMaxHeight { get; private set; }
    //public bool isAlreadyFlyAway = false;
    public int numberOfResetFlyAway = 2;
    public float flyAwayCooldown;
    [HideInInspector]
    public float lastTimeFlyAway;

    protected Player _player;
    
    public EnemyFlyingEyeIdleState idleState { get; private set; }
    public EnemyFlyingEyeMoveState moveState { get; private set; }
    public EnemyFlyingEyeBattleState battleState { get; private set; }
    public EnemyFlyingEyeAttackState attackState { get; private set; }
    public EnemyFlyingEyeStunnedState stunnedState { get; private set; }
    public EnemyFlyingEyeDeadState deadState { get; private set; }
    public EnemyFlyingEyeMeleeAttackState meleeAttackState { get; private set; }
    public EnemyFlyingEyeRunAwayState runAwayState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyFlyingEyeIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyFlyingEyeMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyFlyingEyeBattleState(this, stateMachine, "Move", this);
        attackState = new EnemyFlyingEyeAttackState(this, stateMachine, "Attack", this);
        stunnedState = new EnemyFlyingEyeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new EnemyFlyingEyeDeadState(this, stateMachine, "Die", this);
        meleeAttackState = new EnemyFlyingEyeMeleeAttackState(this, stateMachine, "MeleeAttack", this);
        runAwayState = new EnemyFlyingEyeRunAwayState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start(); 
        _player = PlayerManager.Instance.player;
        initialPosition = this.transform.position;
        currentMaxHeight = initialPosition.y + maxHeightDistance;
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
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Vector2 direction = Quaternion.Euler(0, 0, viewAngle) * transform.right;
        Gizmos.DrawRay(transform.position, direction * viewRadius);
        direction = Quaternion.Euler(0, 0, -viewAngle) * transform.right;
        Gizmos.DrawRay(transform.position, direction * viewRadius);
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    public bool IsPlayerDetectedInHalfOfTheCircle()
    {
        Vector2 directionToPlayer = _player.transform.position - transform.position;
        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

        if (angleToPlayer <= viewAngle && directionToPlayer.magnitude <= viewRadius)
        {
            return true;
        }

        return false;
    }

    public void TriggerRangeAttackAnimation()
    {
        GameObject newProjectile = Instantiate(_projectilePrefab, attackCheck.position, Quaternion.identity);
        Vector2 directionToPlayer = _player.transform.position - transform.position;
        newProjectile.GetComponent<ProjectileController>().SetupProjectile(directionToPlayer, stats);
    }
}
