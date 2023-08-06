using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentObject<GameManager>, ISaveManager
{
    [SerializeField]
    private Checkpoint[] _checkpoints;

    public string closestCheckPointID;
    private Player _player => PlayerManager.Instance.player;
    public bool isPause = false;
    public event System.Action<bool> OnPauseGameChanged; 
    protected override void Start()
    {
        base.Start();
        _checkpoints = FindObjectsOfType<Checkpoint>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            RestartScene();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            PauseGame(isPause);
        }
    }

    public void RestartScene()
    {
        SaveManager.Instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData gameData)
    {
        foreach (KeyValuePair<string, bool> pair in gameData.checkPoints)
        {
            foreach (Checkpoint checkpoint in _checkpoints)
            {
                if (checkpoint.checkPointID == pair.Key && pair.Value == true)
                {
                    checkpoint.ActivateCheckPoint();
                }
            }
        }

        closestCheckPointID = gameData.closestCheckpointID;
        //PlacePlayerAtClosestCheckpoint();
        Invoke(nameof(PlacePlayerAtClosestCheckpoint), 0.1f);
    }

    private void PlacePlayerAtClosestCheckpoint()
    {
        foreach (Checkpoint checkpoint in _checkpoints)
        {
            if (closestCheckPointID == checkpoint.checkPointID)
            {
                _player.transform.position = checkpoint.transform.position;
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log(gameData.closestCheckpointID);
        gameData.closestCheckpointID = FindClosestCheckpoint().checkPointID;
        gameData.checkPoints.Clear();
        
        
        foreach (Checkpoint checkpoint in _checkpoints)
        {
            gameData.checkPoints.Add(checkpoint.checkPointID, checkpoint.isActivated);
        }
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;
        foreach (Checkpoint checkpoint in _checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(_player.transform.position,
                checkpoint.transform.position);
            if (distanceToCheckpoint < closestDistance && checkpoint.isActivated == true)
            {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }

    public void PauseGame(bool isPause)
    {
        Time.timeScale = isPause ? 0 : 1;
        OnPauseGameChanged?.Invoke(isPause);
    }
}
