using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private string targetLayerName = "Player";
    private Vector2 _shootingDirection;
    private Rigidbody2D _rb;
    private bool _flipped;
    private CharacterStats _enemyFlyingEyeStats;
    private Animator _animator;
    private bool _canMove = true;
    private Enemy_FlyingEye _enemyFlyingEye;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_canMove)
            _rb.velocity = _shootingDirection;
    }

    public void SetupProjectile(Vector2 shootingDir, CharacterStats enemyFlyingEyeStats)
    {
        _shootingDirection = shootingDir;
        _enemyFlyingEyeStats = enemyFlyingEyeStats;
        _enemyFlyingEye = _enemyFlyingEyeStats.GetComponent<Enemy_FlyingEye>();
        if (_enemyFlyingEye.facingDirection == -1)
            this.transform.Rotate(0,180,0);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            HitCollision();
            _enemyFlyingEyeStats.DoDamage(col.GetComponent<CharacterStats>());
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            HitCollision();
        }
    }

    private void HitCollision()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _canMove = false;
        _rb.isKinematic = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _animator.SetTrigger("Explode");
    }
    
    public void FlipProjectile()
    {
        if (_flipped)
            return;
        _shootingDirection = _shootingDirection * -1;
        _flipped = true;
        transform.Rotate(0, 180,0);
        targetLayerName = "Enemy";
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
