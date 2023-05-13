using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherScript : MonoBehaviour
{
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
        if(direction.x>=0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        else transform.localScale=new Vector3(-1.0f,1.0f,1.0f);

        float distance = Mathf.Abs(player.transform.position.x-transform.position.x);

        if(distance<1.0f && Time.time>LastShoot+0.25f){
            Shoot();
            LastShoot=Time.time;
        }
    }
    private void Shoot(){
        Debug.Log("shoot");
    }
}
