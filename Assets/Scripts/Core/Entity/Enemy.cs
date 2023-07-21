using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private bool _isAttacking = false;
    [Header("Move info")]
    [SerializeField]
    private float moveSpeed = 2f;

    [Header("Player Detection Info")]
    [SerializeField]
    private float playerCheckDistance;
    [SerializeField]
    private LayerMask playerLayerMask;

    private RaycastHit2D _isPlayerDetected;
    

    protected override void Update()
    {
        base.Update();
        if (_isPlayerDetected)
        {
            if (_isPlayerDetected.distance > 1)
            {
                rigidbody2D.velocity = new Vector2(moveSpeed * (moveSpeed + 0.5f) * facingDirection, rigidbody2D.velocity.y);
                Debug.Log("I see the player");
                _isAttacking = false;
            }
            else
            {
                Debug.Log("Attack " + _isPlayerDetected);
                _isAttacking = true;
            }
        }
        if (IsGroundedDetected() == false || IsWallDetected())
            Flip();
        Move();
    }

    private void Move()
    {
        if (_isAttacking == false)
            rigidbody2D.velocity = new Vector2(moveSpeed * facingDirection, rigidbody2D.velocity.y);
    }

    private void CheckCollision()
    {
        _isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDirection,
            playerLayerMask);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDirection, transform.position.y));
    }
}
