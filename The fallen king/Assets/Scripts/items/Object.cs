using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public GameObject Item;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.getitem);
                Destroy(gameObject);
            }
        }
    }
}
