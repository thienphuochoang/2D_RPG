using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirectoryPath = String.Empty;
    private string dataFileName = String.Empty;
    private bool _wantToEncrypt = false;
    private string codeWord = "[];',./";
    public FileDataHandler(string inputDataDirectoryPath, string inputDataFileName, bool inputEncrypt)
    {
        dataDirectoryPath = inputDataDirectoryPath;
        dataFileName = inputDataFileName;
        _wantToEncrypt = inputEncrypt;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(gameData, true);
            if (_wantToEncrypt)
                dataToStore = EncryptDecryptData(dataToStore);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);
        GameData loadData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = String.Empty;
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if (_wantToEncrypt)
                    dataToLoad = EncryptDecryptData(dataToLoad);
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        return loadData;
    }

    private string EncryptDecryptData(string data)
    {
        string modifiedData = String.Empty;
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
        }

        return modifiedData;
    }
}
