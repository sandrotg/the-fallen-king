using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumirItem : MonoBehaviour
{
    public int ID;
    public ItemType itemType;
    void Awake()
    {

    }
    public bool EatFood(float healthToGive)
    {
        if (PlayerController.instance.GetCurrentHealth() < PlayerController.instance.GetTotalHealth())
        {
            PlayerController.instance.SetSumCurrentHealth(healthToGive);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UsePotion(float healthToGive)
    {
        if (PlayerController.instance.GetCurrentHealth() < PlayerController.instance.GetTotalHealth())
        {
            PlayerController.instance.SetPorcentCurrentHealth(healthToGive);
            return true;
        }
        else
        {
            return false;
        }
    }
    //1 para la comida, 2 para las posiones de vida
    public bool EjecutarConsumo(int ConsumibleType, float healthToGive)
    {
        bool ejecution = false;
        switch (ConsumibleType)
        {
            case 1:
                ejecution = EatFood(healthToGive);
                break;

            case 2:
                ejecution = UsePotion(healthToGive);
                break;
        }
        return ejecution;
    }

    public void EquiparItem(ItemType tipo, float increment){
        if(tipo == ItemType.SWORD){
            SetSwordDamage(increment);
        }else if(tipo == ItemType.ARMOR){
            SetExtraArmor(increment);
        }
    }

    public void SetSwordDamage(float damage){
        PlayerController.instance.swordDamage = damage;
        PlayerController.instance.AddSwordDamage();
    }

    public void SetExtraArmor(float armor){
        PlayerController.instance.extraArmor = armor;
        PlayerController.instance.AddExtraArmor();
    }

    public void DesequiparItem(ItemType tipo){
        if(tipo == ItemType.SWORD){
            SetSwordDamage(0f);
        }else if(tipo == ItemType.ARMOR){
            SetExtraArmor(0f);
        }
    }
}
