using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    [SerializeField] private int _areaSoundIndex;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            AudioManager.Instance.PlayBGMWithTime(_areaSoundIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            AudioManager.Instance.StopAllBGM();
        }
    }
}
