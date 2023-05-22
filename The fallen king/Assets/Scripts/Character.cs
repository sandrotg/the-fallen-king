using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Character : MonoBehaviour
{
    [SerializeField] protected float baseHealth;

    protected float totalHealth;
    [SerializeField] protected float baseDamage;
    protected float totalDamage;
    [SerializeField]protected float currentHealth;
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

}
