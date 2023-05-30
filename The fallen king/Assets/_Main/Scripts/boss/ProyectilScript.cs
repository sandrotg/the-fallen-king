using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilScript : MonoBehaviour
{
    float movespeed = 7f;
    float damage;
    Rigidbody2D rb2d;
    Vector2 moveDirection;
    //PlayerController target;
    private float timeToDestroy = 3f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CambiarAnimacion();
        damage = Boss.instance.GetBaseDamage();
        rb2d = GetComponent<Rigidbody2D>();
        //target = PlayerController.instance;
        moveDirection = (PlayerController.instance.transform.position- transform.position).normalized*movespeed;
        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy == 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            PlayerController.instance.TakeDamage(damage);
        }
    }

    private void CambiarAnimacion(){
        animator.SetInteger("atack", Random.Range(0,7));
    }
    
}
