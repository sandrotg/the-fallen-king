using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRange : MonoBehaviour
{

    public Animator animator;
    public outofrange outofrange;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("atack", true);
            outofrange.atack = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
