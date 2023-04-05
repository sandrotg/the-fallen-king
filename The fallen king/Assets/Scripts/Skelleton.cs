using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelleton : MonoBehaviour
{
    public int routine;
    public float cronometro;
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

    void Start()
    {
    animator = GetComponent<Animator>();
    target = GameObject.Find("Player");   
    }

    public void Behavior()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangeX && !atack)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
            routine = Random.Range(0,2);
            cronometro = 0;
            }
            switch(routine)
            {
                case 0:
                animator.SetBool("walking", false);
                break;

                case 1:
                directionX = Random.Range(0,2);
                routine++;
                break;

                case 2:
                    switch(directionX)
                    {
                        case 0:
                        transform.rotation = Quaternion.Euler(0,0,0);
                        transform.Translate(Vector3.right * Speed_walk * Time.deltaTime);
                        animator.SetBool("walking", true);
                        break;

                        case 1:
                        transform.rotation = Quaternion.Euler(0,180,0);
                        transform.Translate(Vector3.right * Speed_walk * Time.deltaTime);
                        animator.SetBool("walking", true);
                        break;
                    }
                break;
            }
        }       
        if (Mathf.Abs(transform.position.y - target.transform.position.y) > rangeY && !atack)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
            routine = Random.Range(0,2);
            cronometro = 0;
            }
            if(directionX == 0)
            {
                switch(routine)
                {
                    case 0:
                    animator.SetBool("walking", false);
                    break;

                    case 1:
                    directionY = Random.Range(0,2);
                    routine++;
                    break;

                    case 2:
                        switch(directionY)
                        {
                            case 0:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            transform.Translate(Vector3.up * Speed_walk * Time.deltaTime);
                            animator.SetBool("walking", true);
                            break;
                        
                            case 1:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            transform.Translate(Vector3.up * Speed_walk * Time.deltaTime);
                            animator.SetBool("walking", true);
                            break;
                        }
                    break; 
                }   
            }
            else
            {
              switch(routine)
                {
                    case 0:
                    animator.SetBool("walking", false);
                    break;

                    case 1:
                    directionY = Random.Range(0,2);
                    routine++;
                    break;

                    case 2:
                        switch(directionY)
                        {
                            case 0:
                            transform.rotation = Quaternion.Euler(0,180,0);
                            transform.Translate(Vector3.up * Speed_walk * Time.deltaTime);
                            animator.SetBool("walking", true);
                            break;
                        
                            case 1:
                            transform.rotation = Quaternion.Euler(0,180,0);
                            transform.Translate(Vector3.up * Speed_walk * Time.deltaTime);
                            animator.SetBool("walking", true);
                            break;
                        }
                    break;  
                }
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangeA && !atack)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    transform.Translate(Vector3.right * Speed_walk * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    animator.SetBool("atack", false);
                    animator.SetBool("walking", true);
                }
                else
                {
                    transform.Translate(Vector3.right * Speed_walk * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    animator.SetBool("atack", false);
                    animator.SetBool("walking", true);
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
                    animator.SetBool("walking", true);
                }
                else
                {
                    transform.Translate(Vector3.up * Speed_walk * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    animator.SetBool("atack", false);
                    animator.SetBool("walking", true);
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
    }    

    public void finalA()
    {
        animator.SetBool("atack", false);
        atack = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void weapontrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void weaponfalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        Behavior();
    }
}
