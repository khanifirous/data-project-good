using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Save Data")]
    public string m_playername;
    public int m_highScore;

    [Header("Current Game")]
    public string m_cPlayerName;
    public int m_cScore;

  
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string m_playername;
        public int m_highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();

        data.m_playername = m_playername;
        data.m_highScore = m_highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_playername = data.m_playername;
            m_highScore = data.m_highScore;
        }
    }
}

