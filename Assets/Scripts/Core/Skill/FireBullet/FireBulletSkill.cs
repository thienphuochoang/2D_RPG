using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletSkill : Skill
{
    [SerializeField] private GameObject fireBulletPrefab;
    private GameObject currentBullet;
    [SerializeField]
    private Transform spawnPoint;

    [Header("Bullet Info")]
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private float existDuration = 2;

    public event System.Action OnFireBulletSkillCoolDown;
    public override void Activate()
    {
        base.Activate();
        if (currentBullet == null)
        {
            currentBullet = Instantiate(fireBulletPrefab, spawnPoint.position, PlayerManager.Instance.player.transform.rotation);
            FireBullet bullet = currentBullet.GetComponent<FireBullet>();
            bullet.Setup(existDuration, moveSpeed);
            OnFireBulletSkillCoolDown?.Invoke();
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
