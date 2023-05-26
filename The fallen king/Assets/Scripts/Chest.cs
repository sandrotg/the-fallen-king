using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator myAnim;
    public GameObject chestItem;
    public float chestDelay;
    private Collider2D myCollider;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                myAnim.SetBool("opened", true);
                AudioManager.instance.PlayAudio(AudioManager.instance.cofre);
                StartCoroutine(GetChestItem());
                myCollider.enabled = false;
            }
        }
    }

    IEnumerator GetChestItem()
    {
        yield return new WaitForSeconds(chestDelay);
        Instantiate(chestItem, transform.position, Quaternion.identity);
    }
}