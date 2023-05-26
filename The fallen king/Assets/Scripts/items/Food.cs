using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float healthToGive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
         if (collision.CompareTag("Player")){
             if (collision.GetComponent<PlayerController>().GetCurrentHealth() < collision.GetComponent<PlayerController>().GetTotalHealth()){
                collision.GetComponent<PlayerController>().SetSumCurrentHealth(healthToGive);
                AudioManager.instance.PlayAudio(AudioManager.instance.getitem);
                Destroy(gameObject);
             }
         }
    }
}
