using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class lancer : enemy
{
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer > lineOFSite)
        {
            move();
        }
        else
        {
            followPlayer();
        }
    }
}
