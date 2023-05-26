using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtons : MonoBehaviour
{
    Inventory inventory;
    GameManager gameManager;
    ConsumirItem consumir;
    [SerializeField] float healtToGive;
    [SerializeField] float Increase;

    public int itemType;
    // Start is called before the first frame update
    void Start()
    {
        consumir = GetComponent<ConsumirItem>();
        gameManager = GameManager.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }

    public void UseItem()
    {
        if (consumir.itemType != ItemType.USABLE)
        {
            inventory.CheckEquipmentInventory(consumir.itemType);
            if (transform.GetChild(1).gameObject.activeSelf)
            {
                transform.GetChild(1).gameObject.SetActive(false);
                consumir.DesequiparItem(consumir.itemType);
            }
            else
            {
                transform.GetChild(1).gameObject.SetActive(true);
                consumir.EquiparItem(consumir.itemType, Increase);
            }
        }
        else
        {
            bool DeleteItemOrNo = consumir.EjecutarConsumo(itemType, healtToGive);
            if (DeleteItemOrNo == true)
            {
                inventory.UseInventoryItems(gameObject.name);
            }
        }
        // inventory.UseInventoryItems(gameObject.name);
    }
}
