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
    public Transform attackPoint;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;


    // Start is called before the first frame update

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.SetFloat(MOVING, 0);
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        animator.SetFloat(MOVING, isMoving());
        if (Input.GetKeyDown(KeyCode.I)){
            Attack1();
        }
        if (Input.GetKeyDown(KeyCode.J)){
            Attack2();
        }
        if(Input.GetKeyDown(KeyCode.L)){
            Attack3();
        }
    }
    void Move()
    {
        if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
        {
            this.transform.Translate(
                new Vector3(Input.GetAxisRaw(horizontal) * speed * Time.deltaTime, 0, 0));
            if (Input.GetAxis(horizontal) < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetAxis(horizontal) > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        if (Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
        {
            this.transform.Translate(
                new Vector3(0, Input.GetAxisRaw(vertical) * speed * Time.deltaTime, 0));
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
    void Attack1(){
        //Play an attack animation
        animator.SetTrigger("Attack1");
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackrange,enemyLayers);
        // Damage them
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("we hit"+ enemy.name);
        }
    }
    void Attack2(){
        animator.SetTrigger("Attack2");
    }
    void Attack3(){
        animator.SetTrigger("Attack3");
    }
    void OnDrawGizmosSelected(){
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackrange);
    }
}
