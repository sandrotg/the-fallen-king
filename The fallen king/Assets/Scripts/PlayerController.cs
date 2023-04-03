using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 4.0f; 
    private const string vertical = "Vertical";
    private const string horizontal = "Horizontal";
    private const string MOVING = "isMoving";
    Animator animator;
    // Start is called before the first frame update

    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.SetFloat(MOVING,0);
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        animator.SetFloat(MOVING,Mathf.Abs(Input.GetAxisRaw(horizontal)));
    }
    void Move(){
        if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f){
            this.transform.Translate(
                new Vector3(Input.GetAxisRaw(horizontal)*speed*Time.deltaTime,0,0));
            if(Input.GetAxis(horizontal)< 0){
            GetComponent<SpriteRenderer>().flipX = true;
            }
            if(Input.GetAxis(horizontal)> 0){
            GetComponent<SpriteRenderer>().flipX = false;
            }
        }
         if (Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f){
            this.transform.Translate(
                new Vector3(0,Input.GetAxisRaw(vertical)*speed*Time.deltaTime,0));
        }

    }
}