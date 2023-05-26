using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController playerController = col.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.DataToSave();
                playerController.SavePlayerPosition();
                Debug.Log("Game Saved");
            }
        }
    }
}
