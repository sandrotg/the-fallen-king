using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class PlayerController : Character
{
    Vector3 startPosition;
    [SerializeField] Image healthImage;
    public float speed = 4.0f;
    private const string vertical = "Vertical";
    private const string horizontal = "Horizontal";
    private const string MOVING = "isMoving";
    Animator animator;
    public Rigidbody2D playerRigidBody;
    public Transform attackPoint;
    public float attackrange = 0.5f;
    [SerializeField]
    private LayerMask enemyLayers;
    public Vector3 PlayerPosition;
    public GameDataController gameDataController;
    public static PlayerController instance;
    public Canvas gameover;

    // Start is called before the first frame update

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        animator.SetBool("isDead", false);
        animator.SetFloat(MOVING, 0);
        gameover.enabled = false;
        /*totalArmor = baseArmor + extraArmor;
        totalHealth = baseHealth + totalArmor;
        currentHealth = totalHealth;
        totalDamage = baseDamage + swordDamage; 
        startPosition = this.transform.position; */
        if (instance == null)
        {
            instance = this;
        }
    }

    void startGame(){
        this.transform.position = startPosition;
    }


    // Update is called once per frame
    void Update()
    {
        
        healthImage.fillAmount = currentHealth/ totalHealth;
        Move();
        animator.SetFloat(MOVING, isMoving());
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Attack1();
                AudioManager.instance.PlayAudio(AudioManager.instance.player1);
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack2();
                AudioManager.instance.PlayAudio(AudioManager.instance.player2);
                nextAttackTime = Time.time + 1.25f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Attack3();
                AudioManager.instance.PlayAudio(AudioManager.instance.player3);
                nextAttackTime = Time.time + 1.5f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.playershield);
            }
        }

        animator.SetBool("block", Block());

    }
    void Move()
    {
        if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
        {
            /*  this.transform.Translate(
                  new Vector3(Input.GetAxisRaw(horizontal) * speed * Time.deltaTime, 0, 0)); */
            playerRigidBody.velocity = new Vector2(Input.GetAxisRaw(horizontal) * speed, playerRigidBody.velocity.y);
            if (Input.GetAxis(horizontal) < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetAxis(horizontal) > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
        {
            /* this.transform.Translate(
                 new Vector3(0, Input.GetAxisRaw(vertical) * speed * Time.deltaTime, 0)); */
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Input.GetAxisRaw(vertical) * speed);
        }
        if (Mathf.Abs(Input.GetAxisRaw(horizontal)) < 0.5f && Mathf.Abs(Input.GetAxisRaw(vertical)) < 0.5f)
        {
            playerRigidBody.velocity = Vector2.zero;
        }

    }
    float isMoving()
    {
        if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
        {
            return Mathf.Abs(Input.GetAxisRaw(horizontal));
        }
        else
        {
            if (Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
            {
                return Mathf.Abs(Input.GetAxisRaw(vertical));
            }
            else
            {
                return 0;
            }
        }
    }
    void Attack1()
    {
        //Play an attack animation
        animator.SetTrigger("Attack1");
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackrange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (gameObject.tag == "Boss")
            {
                enemy.GetComponent<Boss>().TakeDamage(totalDamage);
            }
            else
            {
                enemy.GetComponent<enemy>().TakeDamage(totalDamage);
            }
        }
    }
    void Attack2()
    {
        animator.SetTrigger("Attack2");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackrange * 1.1f, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (gameObject.tag == "Boss")
            {
                enemy.GetComponent<Boss>().TakeDamage(totalDamage * 1.25f);
            }
            else
            {
                enemy.GetComponent<enemy>().TakeDamage(totalDamage * 1.25f);
            }
        }
    }

    void Attack3()
    {
        animator.SetTrigger("Attack3");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackrange * 1.3f, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (gameObject.tag == "Boss")
            {
                enemy.GetComponent<Boss>().TakeDamage(totalDamage * 1.5f);
            }
            else
            {
                enemy.GetComponent<enemy>().TakeDamage(totalDamage * 1.5f);
            }
        }
    }
    bool Block()
    {
        if (Input.GetKey(KeyCode.K))
        {
            speed = 0;
            return true;
        }
        else
        {
            speed = 4.0f;
            return false;
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");
        if (currentHealth <= 0)
        {
            Die();
            gameover.enabled = true;
        }
    }

    /*protected void Die()
    {
        GameDataController gameDataController = new GameDataController();
        animator.SetBool("isDead", true);
        //healthImage.fillAmount = 0;
        gameDataController.LoadData();
        GetComponent<Collider2D>().enabled = false;
        playerRigidBody.velocity = Vector2.zero;
        this.enabled = false;
    }*/

    protected void Die()
    {
        //animator.SetBool("isDead", true);
        //gameDataController.LoadData();
        //currentHealth = totalHealth;
        //PlayerPosition = gameDataController.gameData.playerPosition;
        //GetComponent<Collider2D>().enabled = false;
        //playerRigidBody.velocity = Vector2.zero;
        //this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackrange);
    }
}
