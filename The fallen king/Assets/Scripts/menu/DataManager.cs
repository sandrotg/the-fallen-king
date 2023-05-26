using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private float[] position;
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void currentHealthData(float value)
    {
        PlayerPrefs.SetFloat("currentHealth", value);
    }

    public void SavingData(float[] data)
    {
        if (data.Length <= 3)
        {
            PlayerPrefs.SetFloat("PlayerPositionX", data[0]);
            PlayerPrefs.SetFloat("PlayerPositionY", data[1]);
            PlayerPrefs.SetFloat("PlayerPositionZ", data[2]);
        }
        else
        {
            Debug.LogError("Data array does not have enough elements!");
        }
    }

    void Update()
    {
        
    }
}
