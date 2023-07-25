using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float coolDown;
    protected float coolDownTimer;

    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        bool canBeActivated = coolDownTimer < 0;
        return canBeActivated;
        /*if (coolDownTimer < 0)
        {
            Activate();
            coolDownTimer = coolDown;
            return true;
        }

        return false;*/
    }

    public virtual void UseSkill()
    {
        Activate();
        coolDownTimer = coolDown;
    }

    public virtual void Activate()
    {
    }
}
