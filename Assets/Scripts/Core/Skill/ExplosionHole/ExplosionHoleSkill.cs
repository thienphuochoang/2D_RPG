using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHoleSkill : Skill
{
    [SerializeField] private GameObject explosionHolePrefab;
    private GameObject currentExplosionHole;
    [SerializeField]
    private Transform spawnPoint;

    [Header("Explosion Hole Info")]
    [SerializeField]
    private float _maxSize;
    [SerializeField]
    private float _growSpeed;
    [SerializeField]
    private float _existDuration;

    public event System.Action OnExplosionHoleSkillCoolDown;
    public override void Activate()
    {
        base.Activate();
        if (currentExplosionHole == null)
        {
            currentExplosionHole = Instantiate(explosionHolePrefab, spawnPoint.position, Quaternion.identity);
            ExplosionHole explosionHole = currentExplosionHole.GetComponent<ExplosionHole>();
            explosionHole.Setup(_maxSize, _growSpeed, true, _existDuration);
            OnExplosionHoleSkillCoolDown?.Invoke();
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
