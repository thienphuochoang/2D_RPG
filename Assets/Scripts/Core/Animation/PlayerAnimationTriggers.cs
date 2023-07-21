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
}
