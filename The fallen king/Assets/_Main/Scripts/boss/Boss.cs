using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
class Boss : enemy
{
    [SerializeField] Transform[] transforms;
    [SerializeField] GameObject proyectil;
    [SerializeField] private float timeToShoot, countDown;
    [SerializeField] private float timeToTP, countDownTP;
    public Image HealthImage;
    public static Boss instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(instance);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        totalHealth = baseHealth + baseArmor;
        currentHealth = totalHealth;
        Debug.Log(currentHealth);
        countDown = timeToShoot;
        countDownTP = timeToTP;
    }

    // Update is called once per frame
    void Update()
    {
        BossScale();
        HealthImage.fillAmount = currentHealth / totalHealth;
        countDown -= Time.deltaTime;
        countDownTP -= Time.deltaTime;
        if (countDown <= 0)
        {
            Shootplayer();
            countDown = timeToShoot;
        }
        if(countDownTP<=0){
            Teleport();
            countDownTP = timeToTP;
        }

    }

    public void Shootplayer()
    {
        enemyAnimator.SetTrigger("projectile");
        GameObject spell = Instantiate(proyectil, transform.position, Quaternion.identity);

    }
    public void Teleport()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
    }
    public void BossScale(){
        if(transform.position.x > PlayerController.instance.transform.position.x){
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if(transform.position.x < PlayerController.instance.transform.position.x){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
