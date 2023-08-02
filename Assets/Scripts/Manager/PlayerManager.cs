using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : PersistentObject<PlayerManager>, ISaveManager
{
    public Player player;
    [SerializeField]
    private int _currentCoins = 0;
    public event System.Action OnCoinsChanged; 
    protected override void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public bool HaveEnoughCoins(int price)
    {
        if (price > _currentCoins)
        {
            Debug.Log("Not enough Coins");
            return false;
        }
        return true;
    }

    public void ConsumeCoins(int price)
    {
        _currentCoins -= price;
        if (_currentCoins <= 0)
            _currentCoins = 0;
        OnCoinsChanged?.Invoke();
    }

    public int GetCurrentCoins() => _currentCoins;
    public void LoadData(GameData gameData)
    {
        this._currentCoins = gameData.coins;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.coins = this._currentCoins;
    }
}
