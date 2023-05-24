using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class enemy : Character
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
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float lineOFSite;
    [SerializeField] protected float lineOfAttack;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected int numAttacks;
    protected float distanceFromPlayer;
    protected const string Move = "isMoving";
    public GameObject enemigo;
    private string enemyTag;
    public GameObject[] loot;

    
    void Start()
    {
        init();
        enemyTag = enemigo.tag;
    }

    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer > lineOFSite)
        {
            move();
        }
        else
        {
            if (distanceFromPlayer > lineOfAttack)
            {
                followPlayer();
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack(numAttacks);
                    if (enemyTag == "skelleton")
                    {
                        AudioManager.instance.PlayAudio(AudioManager.instance.skelsword);
                    }
                    else if (enemyTag == "lancer")
                    {
                        AudioManager.instance.PlayAudio(AudioManager.instance.skellancer);
                    }
                    else if (enemyTag == "butcher")
                    {
                        AudioManager.instance.PlayAudio(AudioManager.instance.skelbutcher);
                    }
                }
            }
        }
    }

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
                AudioManager.instance.PlayAudio(AudioManager.instance.skelwalk);
                timeToMakeStepCounter = timeBetweenSteps;
                directionToMakeStep = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * enemySpeed;
            }
        }
        enemyAnimator.SetBool(Move, Moving());
        if (enemyRigidBody.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (enemyRigidBody.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
                transform.rotation = Quaternion.Euler(0, 0, 0);
                AudioManager.instance.PlayAudio(AudioManager.instance.skelwalk);
            }
            if (transform.position.x > player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                AudioManager.instance.PlayAudio(AudioManager.instance.skelwalk);
            }
        }

    }

    protected void Attack(int numAttacks)
    {
        Collider2D player;
        {
            if (distanceFromPlayer <= lineOfAttack)
            {
                int attackGenerator = Random.Range(1, numAttacks + 1);
                switch (attackGenerator)
                {
                    case 1:
                        enemyAnimator.SetTrigger("attack" + string.Concat(attackGenerator));
                        player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
                        player.GetComponent<PlayerController>().TakeDamage(baseDamage);
                        nextAttackTime = Time.time + 1f / attackRate;
                        break;
                    case 2:
                        enemyAnimator.SetTrigger("attack" + string.Concat(attackGenerator));
                        player = Physics2D.OverlapCircle(attackPoint.position, attackRange * 1.25f, playerLayer);
                        player.GetComponent<PlayerController>().TakeDamage(baseDamage * 1.3f);
                        nextAttackTime = Time.time + 1.25f / attackRate;
                        break;
                    case 3:
                        enemyAnimator.SetTrigger("attack" + string.Concat(attackGenerator));
                        break;
                    case 4:
                        enemyAnimator.SetTrigger("attack" + string.Concat(attackGenerator));
                        break;
                }
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
        currentHealth = baseHealth;
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        AudioManager.instance.PlayAudio(AudioManager.instance.skelhit);
        enemyAnimator.SetTrigger("hit");
        if (currentHealth <= 0)
        {
            Die();
            Instantiate(loot[Random.Range(0, loot.Length)], transform.position, Quaternion.identity);
        }
    }

    protected void Die()
    {
        enemyAnimator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        enemyRigidBody.velocity = Vector2.zero;
        this.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, lineOFSite);
        Gizmos.DrawWireSphere(transform.position, lineOfAttack);
    }
}
