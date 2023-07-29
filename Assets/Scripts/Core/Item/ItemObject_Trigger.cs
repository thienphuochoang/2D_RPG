using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject _itemObject => GetComponentInParent<ItemObject>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            _itemObject.PickUpItem();
        }
    }
}
