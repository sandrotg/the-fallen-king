using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Character : MonoBehaviour
{
    [SerializeField]protected float baseHealth;

    [SerializeField]protected float baseArmor;

    public float extraArmor = 0f;

    [SerializeField] protected float totalArmor;

    [SerializeField] protected float totalHealth;
    [SerializeField]protected float baseDamage;

    public float swordDamage = 0f;
    [SerializeField] protected float totalDamage;
    public float currentHealth;
    protected float nextAttackTime = 0f;
    [SerializeField] protected float attackRate = 2f;

    public void SetPorcentCurrentHealth(float healthToGive){
        currentHealth += healthToGive*totalHealth/100;
        if(currentHealth > totalHealth){
            currentHealth = totalHealth;
        }
    }

    public void SetSumCurrentHealth(float healthToGive){
        currentHealth += healthToGive;
        if(currentHealth > totalHealth){
            currentHealth = totalHealth;
        }
    }
    public float GetCurrentHealth(){
        return currentHealth;
    }

    public float GetTotalHealth(){
        return totalHealth;
    }

    public void AddSwordDamage(){
        totalDamage = baseDamage + swordDamage;
    }
    public void AddExtraArmor(){
        totalArmor = baseArmor + extraArmor;
        totalHealth = baseHealth + totalArmor;
    }
    public void SetBaseArmor(int value){
        baseArmor = value;
    }

    public void SetBaseDamage(int value){
        baseDamage = value;
    }

    public void SetBaseHealth(int value){
        baseHealth = value;
    }


    public float getTotalDamage(){
        return totalDamage;
    }    

    public float getTotalArmor(){
        return totalArmor;
    }
    
}
