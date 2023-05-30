using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Animator enemyAnimator;
    protected GameObject player;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float lineOFSite;
    [SerializeField] protected float lineOfAttack;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected int numAttacks;
    protected float distanceFromPlayer;
    protected const string Move = "isMoving";
    [SerializeField] protected int experienceToAdd;
    public GameObject EnemyItem;
    void Start()
    {
        init();
    }

    void Update()
    {
        if (currentHealth > 0)
        {
            enemyAnimator.SetBool("isDead", false);
        }
        if (GameManager.instance.currentGameState == GameState.inGame)
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
                        if(gameObject.tag == "Skelleton")
                        {
                           Attack(numAttacks);
                           AudioManager.instance.PlayAudio(AudioManager.instance.skelsword);
                        }
                        else
                        {
                            if (gameObject.tag == "Lancer")
                            {
                                Attack(numAttacks);
                                AudioManager.instance.PlayAudio(AudioManager.instance.skellancer);
                            }
                            else
                            {
                                if (gameObject.tag == "Butcher")
                                {
                                    Attack(numAttacks);
                                    AudioManager.instance.PlayAudio(AudioManager.instance.skelbutcher);
                                }
                                else
                                {
                                    if (gameObject.tag == "Bloody Knight")
                                    {
                                        Attack(numAttacks);
                                        AudioManager.instance.PlayAudio(AudioManager.instance.player2);
                                    }
                                }
                            }
                        }
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
            }
            if (transform.position.x > player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

    }

    protected void Attack(int numAttacks)
    {
        Collider2D player;
        if (distanceFromPlayer <= lineOfAttack)
        {
            int attackGenerator = Random.Range(1, numAttacks + 1);
            switch (attackGenerator)
            {
                case 1:
                    enemyAnimator.SetTrigger("attack" + string.Concat(attackGenerator));
                    player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
                    if(player != null){
                        player.GetComponent<PlayerController>().TakeDamage(baseDamage);
                    }
                    nextAttackTime = Time.time + 1f / attackRate;
                    break;
                case 2:
                    enemyAnimator.SetTrigger("attack" + string.Concat(attackGenerator));
                    player = Physics2D.OverlapCircle(attackPoint.position, attackRange * 1.25f, playerLayer);
                    if(player != null){
                        player.GetComponent<PlayerController>().TakeDamage(baseDamage * 1.3f);
                    }
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
    protected void init()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        player = GameObject.Find("Player");
        totalHealth = baseHealth + baseArmor;
        currentHealth = totalHealth;
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
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        enemyAnimator.SetTrigger("hit");
        if (currentHealth == 0)
        {
            Die();
            //Instantiate(EnemyItem, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        if(currentHealth == 0)
        {
            if(gameObject.tag == "Boss")
            {
                SceneManager.LoadScene(4);
            }
            else
            {
            enemyAnimator.SetBool("isDead", true);
            GetComponent<Collider2D>().enabled = false;
            PlayerController.instance.GetComponent<LevelController>().AddExperience(experienceToAdd);
            enemyRigidBody.velocity = Vector2.zero;
            this.enabled = false;
            }
        }
        else
        {
            enemyAnimator.SetBool("isDead", false);
            GetComponent<Collider2D>().enabled = true;
            enemyRigidBody.velocity = directionToMakeStep;;
            this.enabled = true;
        }
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
