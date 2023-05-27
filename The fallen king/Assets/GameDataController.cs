using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameDatas
{
    public Vector3 playerPosition;
}

public class GameDataController : MonoBehaviour
{
    public GameObject player;
    public string GameDataFiles;
    public GameDatas gameData;
    private float healthsaved;

    private void Awake()
    {
        GameDataFiles = Application.dataPath + "/game.data.json";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveData();
            healthsaved = player.GetComponent<PlayerController>().currentHealth;
            Debug.Log("Player's health: " + healthsaved);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadData();
            player.GetComponent<PlayerController>().currentHealth = healthsaved;
        }
    }

    public void SaveData()
    {
        gameData = new GameDatas()
        {
            playerPosition = player.transform.position
        };

        string jsonString = JsonUtility.ToJson(gameData);
        File.WriteAllText(GameDataFiles, jsonString);

        Debug.Log("Game Saved");
    }

    public void LoadData()
{
        if (File.Exists(GameDataFiles))
        {
            string jsonString = File.ReadAllText(GameDataFiles);
            GameDatas loadedData = JsonUtility.FromJson<GameDatas>(jsonString);
            gameData.playerPosition = loadedData.playerPosition;
            player.transform.position = gameData.playerPosition;
            Debug.Log("Player's position: " + gameData.playerPosition);
        }
    }
}
