using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PersistanceManager : MonoBehaviour
{

    [System.Serializable]
    public class SaveData
    {
        public string Name = "";
        public int Score = 0;
    }

    public static PersistanceManager Instance { get; private set; }

    public SaveData PersistentData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadDataLocally();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver(int newScore, string name)
    {
        if (newScore > PersistentData.Score)
        {
            PersistentData = new SaveData();
            PersistentData.Score = newScore;
            PersistentData.Name = name;
            SaveDataLocally();
        }
    }

    private void SaveDataLocally()
    {
        string json = JsonUtility.ToJson(PersistentData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadDataLocally()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            PersistentData = data;
        }
        else
        {
            PersistentData = new SaveData();
        }
    }
}
