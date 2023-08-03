using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int coins;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentID;
    public GameData()
    {
        this.coins = 0;
        inventory = new SerializableDictionary<string, int>();
        equipmentID = new List<string>();
    }
    
}
