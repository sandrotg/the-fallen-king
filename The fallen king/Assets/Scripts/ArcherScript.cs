using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherScript : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject player;
    private float LastShoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   private void Update()
    {
        Vector3 direction = player.transform.position-transform.position;
        if(direction.x>=0.0f) transform.localScale = new Vector3(4.825575f,3.851821f,1f);
        else transform.localScale=new Vector3(-4.825575f,3.851821f,1f);

        float distance = Mathf.Abs(player.transform.position.x-transform.position.x);

        if(distance<1.0f && Time.time>LastShoot+0.25f){
            Shoot();
            LastShoot=Time.time;
        }
    }
    private void Shoot(){
        Debug.Log("shoot");
        Vector3 direction;
        if(transform.localScale.x==1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet= Instantiate(Bullet, transform.position + direction*0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
}
