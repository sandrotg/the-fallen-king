using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> backPack = new List<GameObject>();
    private bool isInstantiated;
    public ItemList itemList;

    Text texto;

    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    private void Start()
    {
        if (itemList != null)
        {

            DataToInventory();
        }
    }

    public void CheckSlotAvailability(GameObject itemToAdd, string itemName, int itemAmount)
    {
        isInstantiated = false;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                slots[i].GetComponent<SlotScript>().isUsed = true;
            }
            else if (!isInstantiated && !slots[i].GetComponent<SlotScript>().isUsed)
            {
                //crear el item en el slot vacio
                if (!inventoryItems.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, slots[i].transform.position, Quaternion.identity);
                    item.transform.SetParent(slots[i].transform, false);
                    item.transform.localPosition = new Vector3(0, 0, 0);
                    item.name = item.name.Replace("(Clone)", "");
                    isInstantiated = true;
                    slots[i].GetComponent<SlotScript>().isUsed = true;
                    inventoryItems.Add(itemName, itemAmount);
                    texto = slots[i].GetComponentInChildren<Text>();
                    texto.text = itemAmount.ToString();
                    break;
                }
                else
                {
                    for (int j = 0; j < slots.Count; j++)
                    {
                        if (slots[j].transform.GetChild(0).gameObject.name == itemName)
                        {
                            //ya lo tenemos asi que sumamos la cantidad de items
                            inventoryItems[itemName] += itemAmount;
                            texto = slots[j].GetComponentInChildren<Text>();
                            texto.text = inventoryItems[itemName].ToString();
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    public void UseInventoryItems(string itemName)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].transform.GetChild(0).gameObject.name == itemName)
            {
                texto = slots[i].GetComponentInChildren<Text>();
                inventoryItems[itemName]--;
                texto.text = inventoryItems[itemName].ToString();
                if (inventoryItems[itemName] <= 0)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].GetComponent<SlotScript>().isUsed = false;
                    inventoryItems.Remove(itemName);
                    ReorganizeInventory();
                }
                break;
            }
        }

    }

    private void ReorganizeInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].GetComponent<SlotScript>().isUsed)
            {
                for (int j = i + 1; j < slots.Count; j++)
                {
                    if (slots[j].GetComponent<SlotScript>().isUsed)
                    {
                        Transform itemToMove = slots[j].transform.GetChild(0).transform;
                        itemToMove.transform.SetParent(slots[i].transform, false);
                        itemToMove.transform.localPosition = new Vector3(0, 0, 0);
                        slots[i].GetComponent<SlotScript>().isUsed = true;
                        slots[j].GetComponent<SlotScript>().isUsed = false;
                        break;
                    }
                }
            }
        }
    }

    public void CheckEquipmentInventory(ItemType type)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponent<SlotScript>().isUsed)
            {
                if (slots[i].transform.GetComponentInChildren<ConsumirItem>().itemType != ItemType.USABLE)
                {
                    if (slots[i].transform.GetComponentInChildren<ConsumirItem>().itemType == type)
                    {
                        if (slots[i].transform.GetChild(0).GetChild(1).gameObject.activeSelf)
                        {
                            slots[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public void InventoryToData()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponent<SlotScript>().isUsed)
            {
                if (!GameData.instance.saveData.goToAddId.Contains(slots[i].GetComponentInChildren<ConsumirItem>().ID))
                {
                    GameData.instance.saveData.goToAddId.Add(slots[i].GetComponentInChildren<ConsumirItem>().ID);
                    GameData.instance.saveData.inventoryItemsName.Add(slots[i].GetComponentInChildren<ConsumirItem>().name);
                    GameData.instance.saveData.inventoryItemsAmount.Add(inventoryItems[slots[i].GetComponentInChildren<ConsumirItem>().name]);
                }
            }
        }
    }

    public void DataToInventory()
    {
        for (int i = 0; i < GameData.instance.saveData.goToAddId.Count; i++)
        {
            for (int j = 0; j < itemList.items.Count; j++)
            {
                if (itemList.items[j].ID == GameData.instance.saveData.goToAddId[i])
                {
                    CheckSlotAvailability(itemList.items[j].gameObject, GameData.instance.saveData.inventoryItemsName[i], GameData.instance.saveData.inventoryItemsAmount[i]);
                }
            }

        }
    }
}
