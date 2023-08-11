using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectileController : MonoBehaviour
{
    private string targetLayerName = "Player";
    private Rigidbody2D _rb;
    private CharacterStats _enemyGoblinStats;
    private Animator _animator;
    private Enemy_Goblin _enemyGoblin;
    private Vector3 _playerPosition;


    public float firingAngle = 45.0f;

    private void Start()
    {
        _playerPosition = PlayerManager.Instance.player.transform.position;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        float target_Distance = Vector3.Distance(transform.position, _playerPosition);
        Vector2 direction = (_playerPosition - transform.position).normalized;
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

        // Extract the X Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector2 vel = new Vector2(Vx * direction.x, Vy) ;
        
        _rb.AddForce(vel,ForceMode2D.Impulse);
    }

    private void Update()
    {
    }

    public void SetupProjectile(Vector3 playerPosition, CharacterStats enemyGoblinStats)
    {
        _enemyGoblinStats = enemyGoblinStats;
        _playerPosition = playerPosition;
        _enemyGoblin = _enemyGoblinStats.GetComponent<Enemy_Goblin>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            HitCollision();
            _enemyGoblinStats.DoDamage(col.GetComponent<CharacterStats>());
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            HitCollision();
        }
    }

    private void HitCollision()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        _rb.isKinematic = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _animator.SetTrigger("Explode");
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
