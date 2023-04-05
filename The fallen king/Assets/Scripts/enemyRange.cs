using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRange : MonoBehaviour
{

    public Animator animator;
    public Skelleton skelleton;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            animator.SetBool("walking", false);
            animator.SetBool("atack", true);
            skelleton.atack = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
