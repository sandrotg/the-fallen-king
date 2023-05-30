using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    public List<int> goToAddId = new List<int>();
    public List<int> inventoryItemsAmount = new List<int>();
    public List<string> inventoryItemsName = new List<string>();
}

public class GameData : MonoBehaviour
{
    public SaveData saveData;
    public static GameData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        if(File.Exists(Application.persistentDataPath + "Player.dat")){
            Load();
        }else{
            Save();
        }
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
        SaveData data = new SaveData();
        data = saveData;
        formatter.Serialize(file, data);
        file.Close();
        print("Data Saved");
    }

    public void Load(){
        if(File.Exists(Application.persistentDataPath + "Player.dat")){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "Player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            print("Data loaded");
        }
    }

    public void ClearData(){
         if(File.Exists(Application.persistentDataPath + "Player.dat")){
            File.Delete(Application.persistentDataPath + "Player.dat");
         }
    }

    public void ClearAllDataList(){
        saveData.goToAddId.Clear();
        saveData.inventoryItemsName.Clear();
        saveData.inventoryItemsAmount.Clear();
        Save();
    }
}
