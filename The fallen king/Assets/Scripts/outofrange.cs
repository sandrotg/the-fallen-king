using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outofrange : MonoBehaviour
{
    public Animator animator;
    public int directionX;
    public int directionY;
    public int Speed_walk;
    public GameObject target;
    public bool atack;
    public float rangeX;
    public float rangeY;
    public float rangeA;
    public GameObject rango;
    public GameObject Hit;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player"); 
    }

    public void Behavior()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangeA && !atack)
        {
            if (transform.position.x < target.transform.position.x)
            {
                transform.Translate(Vector3.right * Speed_walk * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0,0,0);
                animator.SetBool("atack", false);
                animator.SetBool("isMoving", true);
            }
            else
            {
            transform.Translate(Vector3.right * Speed_walk * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0,180,0);
            animator.SetBool("atack", false);
            animator.SetBool("isMoving", true);
            }
        }
        else
        {
            if (!atack)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0,0,0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0,180,0);
                }
                    
            }
        }
        if (Mathf.Abs(transform.position.y - target.transform.position.y) > rangeA && !atack)
        {
            if (transform.position.y < target.transform.position.y)
            {
                transform.Translate(Vector3.up * Speed_walk * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0,0,0);
                animator.SetBool("atack", false);
                animator.SetBool("isMoving", true);
            }
            else
            {
                transform.Translate(Vector3.up * Speed_walk * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0,0,0);
                animator.SetBool("atack", false);
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            if (!atack)
            {
                if (transform.position.y < target.transform.position.y)
                {
                    transform.rotation = Quaternion.Euler(0,0,0);
                }                   
            }
        }
    }   

    public void finalA()
    {
        animator.SetBool("atack", false);
        atack = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    void Update()
    {
        Behavior();
    }
}
