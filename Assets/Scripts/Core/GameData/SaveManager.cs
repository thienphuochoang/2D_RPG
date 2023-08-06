using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : PersistentObject<SaveManager>
{
    private GameData _gameData;
    private string fileName = "Autosave.save";
    private List<ISaveManager> _saveManager;
    private FileDataHandler _dataHandler;
    [SerializeField] private bool _wantToEncryptData = false;

    protected override void Start()
    {
        base.Start();
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, _wantToEncryptData);
        _saveManager = FindAllSaveManagers();
        LoadGame();
    }

    public void StartNewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();
        if (this._gameData == null)
            StartNewGame();

        foreach (var save in _saveManager)
        {
            save.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var save in _saveManager)
        {
            save.SaveData(ref _gameData);
        }
        _dataHandler.Save(_gameData);
    }

    public void DeleteSaveData()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, _wantToEncryptData);
        _dataHandler.Delete();
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        //SaveGame();
    }
    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagersList = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagersList);
    }

    public bool HasSavedData()
    {
        if (_dataHandler.Load() != null)
        {
            return true;
        }

        return false;
    }
}
