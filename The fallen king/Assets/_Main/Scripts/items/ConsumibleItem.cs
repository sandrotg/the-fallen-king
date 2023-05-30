using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// sirve porqueria 
public enum ItemType{
    USABLE,
    SWORD,
    ARMOR,
    SHIELD
}
public class ConsumibleItem : MonoBehaviour
{
    public ItemType itemType;
    public GameObject itemToAdd;
    public int amountToAdd;
    Inventory inventory;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        inventory = gameManager.GetComponent<Inventory>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.getitem);
            inventory.CheckSlotAvailability(itemToAdd, itemToAdd.name, amountToAdd);
            /* if (collision.GetComponent<PlayerController>().GetCurrentHealth() < collision.GetComponent<PlayerController>().GetTotalHealth())
             {
                 collision.GetComponent<PlayerController>().SetPorcentCurrentHealth(healthToGive);
                 Destroy(gameObject);
             } */

            Destroy(gameObject);

        }
    }
}
