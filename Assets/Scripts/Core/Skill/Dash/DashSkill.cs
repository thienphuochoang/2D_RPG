using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    public event System.Action OnDashSkillCoolDown; 
    public override void Activate()
    {
        base.Activate();
        OnDashSkillCoolDown?.Invoke();
    }
}
