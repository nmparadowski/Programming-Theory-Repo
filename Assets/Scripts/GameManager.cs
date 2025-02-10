using System;
using System.IO;
using UnityEngine;

//INHERITANCE
public class GameManager : Singleton<GameManager>
{
    [Serializable]
    public class SaveData
    {
        public string playerName;
        public int highScore;
    }

    private const string ScoreTitle = "Best Score: ";

    private SaveData lastSavedData;

    public string currentPlayer { get; private set; }


    //Loads last saved game data if such exists
    protected override void Awake()
    {
        base.Awake();
        LoadGameData();
    }

    #region Game data management
    //Loads game data from persistent path if such exist - otherwise, create new empty data.
    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            lastSavedData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            lastSavedData = new SaveData();
        }
    }

    //Tries to save new data, if score i greather than the last high score.
    public void SaveGameData(int newPlayerScore)
    {
        if (newPlayerScore > lastSavedData.highScore)
        {
            lastSavedData.highScore = newPlayerScore;
            lastSavedData.playerName = currentPlayer;

            string json = JsonUtility.ToJson(lastSavedData);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    } 

    //Assigns new name for current player
    public void SetNewPlayerName(string newPlayerName)
    {
        currentPlayer = newPlayerName;
    }
    #endregion

    //Returns phrase with high score
    public string GetBestScoreText()
    {
        return $"{ScoreTitle}{Instance.lastSavedData.playerName} : {Instance.lastSavedData.highScore}";
    }
}
