using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player _thePlayer => GetComponentInParent<Player>();

    private void TriggerAnimation()
    {
        _thePlayer.TriggerAnimation();
    }

    private void TriggerAttack()
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(_thePlayer.attackCheck.position, _thePlayer.attackCheckRadius);
        foreach (Collider2D hitObj in colliders)
        {
            if (hitObj.GetComponent<Enemy>() != null)
                hitObj.GetComponent<Enemy>().Damage();
        }
    }
}
