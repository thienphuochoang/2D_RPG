using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<CharacterStats>() != null)
        {
            col.GetComponent<CharacterStats>().KillEntity();
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}
