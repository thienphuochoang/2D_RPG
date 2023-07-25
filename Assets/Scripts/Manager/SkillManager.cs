using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : PersistentObject<SkillManager>
{
    public DashSkill dash { get; private set; }
    public FireBulletSkill fireBulletSkill { get; private set; }
    public ExplosionHoleSkill explosionHoleSkill { get; private set; }

    protected override void Start()
    {
        base.Start();
        dash = GetComponent<DashSkill>();
        fireBulletSkill = GetComponent<FireBulletSkill>();
        explosionHoleSkill = GetComponent<ExplosionHoleSkill>();
    }
}
