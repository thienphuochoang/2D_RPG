using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    private Player _thePlayer;

    private void Start()
    {
        _thePlayer = GetComponent<Player>();
    }

    private void TriggerAnimation()
    {
    }
}
