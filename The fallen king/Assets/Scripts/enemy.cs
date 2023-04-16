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
    protected GameObject player;
    [SerializeField] protected float lineOFSite;
    protected float distanceFromPlayer;
    protected const string Move = "isMoving";

    protected void move()
    {
        enemySpeed = 1;
        if (isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            enemyRigidBody.velocity = directionToMakeStep;
            if (timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                enemyRigidBody.velocity = Vector2.zero;
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;
            if (timeBetweenStepsCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeBetweenSteps;
                directionToMakeStep = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * enemySpeed;
            }
        }
        enemyAnimator.SetBool(Move, Moving());
        if (enemyRigidBody.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        if (enemyRigidBody.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
    protected void followPlayer()
    {
        enemySpeed = 3;
        if (distanceFromPlayer < lineOFSite)
        {
            Vector2 directionToFollow = player.transform.position - transform.position;
            transform.position += (Vector3)directionToFollow.normalized * Time.deltaTime * enemySpeed;
            enemyAnimator.SetBool(Move, true);
            if (transform.position.x < player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            if (transform.position.x > player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0,180,0);
            }
        }

    }
    protected void init()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        player = GameObject.Find("Player");
    }
    protected bool Moving()
    {
        if (Mathf.Abs(enemyRigidBody.velocity.x) > 0.1f || Mathf.Abs(enemyRigidBody.velocity.y) > 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, lineOFSite);
    }
}
