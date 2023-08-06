using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private string targetLayerName = "Player";
    private Vector2 _shootingDirection;
    [SerializeField] private float _xVelocity;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool _flipped;
    private CharacterStats _enemyFlyingEyeStats;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rb.velocity = _shootingDirection;
    }

    public void SetupProjectile(Vector2 shootingDir, CharacterStats enemyFlyingEyeStats)
    {
        _shootingDirection = shootingDir;
        _enemyFlyingEyeStats = enemyFlyingEyeStats;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            //col.GetComponent<CharacterStats>()?.TakeDamage(_damage);
            _enemyFlyingEyeStats.DoDamage(col.GetComponent<CharacterStats>());
            Destroy(this.gameObject);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
        }
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
}
