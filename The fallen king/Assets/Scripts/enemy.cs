using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] protected float enemySpeed = 1;
    protected Rigidbody2D enemyRigidBody;
    protected bool isMoving;
    [SerializeField] protected float timeBetweenSteps;
    protected float timeBetweenStepsCounter;
    [SerializeField] protected float timeToMakeStep;
    protected float timeToMakeStepCounter;
    [SerializeField] protected Vector2 directionToMakeStep;
    protected Animator enemyAnimator;
    protected const string horizontal = "Horizontal";
    protected const string vertical = "Vertical";
    protected const string Move = "isMoving";
 
    protected void move(){
        if(isMoving){
            timeToMakeStepCounter -= Time.deltaTime;
            enemyRigidBody.velocity = directionToMakeStep;
            if(timeToMakeStepCounter<0){
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                enemyRigidBody.velocity = Vector2.zero;
            }
        }else{
            timeBetweenStepsCounter -= Time.deltaTime;
            if(timeBetweenStepsCounter<0){
                isMoving = true;
                timeToMakeStepCounter = timeBetweenSteps;
                directionToMakeStep = new Vector2(Random.Range(-1,2), Random.Range(-1,2)) * enemySpeed;
            }
        }
        enemyAnimator.SetBool(Move, Moving());
        if(enemyRigidBody.velocity.x <0){
             GetComponent<SpriteRenderer>().flipX = true;
        }
        if(enemyRigidBody.velocity.x > 0){
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    protected void init(){
              enemyRigidBody = GetComponent<Rigidbody2D>(); 
        enemyAnimator = GetComponent<Animator>();
        timeBetweenStepsCounter = timeBetweenSteps;
        timeToMakeStepCounter = timeToMakeStep;  
    }
    protected bool Moving(){
        if(Mathf.Abs(enemyRigidBody.velocity.x) > 0.1f || Mathf.Abs(enemyRigidBody.velocity.y) > 0.1f){
            return true;
        }else{
            return false;
        }
    }
    protected void OnCollisionEnter(Collider2D collision){
        if(collision.tag == "Player"){
            PlayerController controller = collision.GetComponent<PlayerController>();
          //  controller.playerRigidBody.;
        }
    }
}
