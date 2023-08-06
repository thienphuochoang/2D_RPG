using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite _activeTorchImage;
    private SpriteRenderer _sr;
    public bool isActivated = false;
    public string checkPointID;
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }
    
    [ContextMenu("Generate Checkpoint ID")]
    private void GenerateCheckPointID()
    {
        checkPointID = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            ActivateCheckPoint();
        }
    }

    public void ActivateCheckPoint()
    {
        _sr.sprite = _activeTorchImage;
        isActivated = true;
    }
}
