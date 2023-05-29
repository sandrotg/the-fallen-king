using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameDatas
{
    public Vector3 playerPosition;
    public float healthsaved;
    public float[] enemyhealthsaved;
    public Vector3[] enemyPosition;
}

public class GameDataController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemy;
    public string GameDataFiles;
    public GameDatas gameData;
    private float[] enemyhealthsaved;
    private Vector3[] enemyPosition;
    private int enemycant;

    private void Awake()
    {
        GameDataFiles = Application.dataPath + "/game.data.json";
        player = GameObject.FindGameObjectWithTag("Player");
        enemycant = enemy.Length;
        enemyhealthsaved = new float[enemycant];
        enemyPosition = new Vector3[enemycant];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadData();
        }
    }

    public void SaveData()
    {
        gameData = new GameDatas()
        {
            playerPosition = player.transform.position,
            healthsaved = player.GetComponent<PlayerController>().currentHealth,
            enemyhealthsaved = new float[enemycant],
            enemyPosition = new Vector3[enemycant]
        };
        for (int i = 0; i < enemycant; i++)
        {
            enemyhealthsaved[i] = enemy[i].GetComponent<enemy>().currentHealth;
            enemyPosition[i] = enemy[i].transform.position;
            Debug.Log("Enemy " + i + " health: " + enemyhealthsaved[i]);
        }
        gameData.enemyhealthsaved = enemyhealthsaved;
        gameData.enemyPosition = enemyPosition;
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
            gameData.healthsaved = loadedData.healthsaved;
            gameData.playerPosition = loadedData.playerPosition;
            gameData.enemyhealthsaved = loadedData.enemyhealthsaved;
            gameData.enemyPosition = loadedData.enemyPosition;

            for (int i = 0; i < enemycant; i++)
            {
                enemy[i].GetComponent<enemy>().currentHealth = gameData.enemyhealthsaved[i];
                enemy[i].transform.position = gameData.enemyPosition[i];
                Debug.Log("Enemy " + i + " health: " + enemyhealthsaved[i]);
                enemy[i].GetComponent<enemy>().Die();
            }
            player.GetComponent<PlayerController>().currentHealth = gameData.healthsaved;
            player.transform.position = gameData.playerPosition;
            Debug.Log("Player's position: " + gameData.playerPosition);
        }
    }
}