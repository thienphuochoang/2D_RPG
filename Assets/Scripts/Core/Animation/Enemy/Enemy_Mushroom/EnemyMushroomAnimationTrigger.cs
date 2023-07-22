using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomAnimationTrigger : MonoBehaviour
{
    private Enemy_Mushroom _enemy => GetComponentInParent<Enemy_Mushroom>();

    private void TriggerAnimation()
    {
        _enemy.TriggerAnimation();
    }
}
