using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lancer : enemy
{
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}