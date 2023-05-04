using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float baseHealth;
    [SerializeField]
    protected float baseDamage;
    protected float currentHealth;


}
