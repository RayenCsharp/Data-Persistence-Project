using System.IO;
using UnityEngine;

public class DataHolder : MonoBehaviour
{

    public static DataHolder Instance;

    public int bestScore;
    public string currentPlayerName;
    public string bestPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string currentPlayerName;
        public string bestPlayer;
    }

    public void Save()
    {
        SaveData data = new SaveData
        {
            bestScore = this.bestScore,
            currentPlayerName = this.currentPlayerName,
            bestPlayer = this.bestPlayer
        };
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.bestScore;
            currentPlayerName = data.currentPlayerName;
            bestPlayer = data.bestPlayer;
        }
        else
        {
            Debug.LogWarning("Save file not found at " + path);
        }
    }
}
