using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public GameObject bossGO;

    void Start(){
        bossGO.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossUI.instance.BossActivation();
            StartCoroutine(WaitForBoss());

        }
    }
    IEnumerator WaitForBoss()
    {
        var currentSpeed = PlayerController.instance.speed;
        PlayerController.instance.speed = 0;
        bossGO.SetActive(true);
        yield return new WaitForSeconds(3f);
        PlayerController.instance.speed = currentSpeed;
        Destroy(gameObject);
    }
}
