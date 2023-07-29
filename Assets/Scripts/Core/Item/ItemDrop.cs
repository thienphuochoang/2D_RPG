using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int maximumDropItems;
    [SerializeField] private ItemData[] possibleDropList;
    [SerializeField] private List<ItemData> dropList = new List<ItemData>();
    
    [SerializeField] private GameObject dropItemPrefab;

    public void GenerateDropList()
    {
        for (int i = 0; i < possibleDropList.Length; i++)
        {
            possibleDropList[i].dropChance = Random.Range(0, 100);
            dropList.Add(possibleDropList[i]);
        }

        /*for (int i = 0; i < maximumDropItems; i++)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];
            dropList.Remove(randomItem);
            DropItem(randomItem);
        }*/
    }
    public void DropItem()
    {
        for (int i = 0; i < maximumDropItems; i++)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];
            dropList.Remove(randomItem);
            GameObject newDropItem = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
            Vector2 randomDropVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));
            newDropItem.GetComponent<ItemObject>().SetupItem(randomItem, randomDropVelocity);
        }

    }

}
