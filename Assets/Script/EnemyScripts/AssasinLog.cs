using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssasinLog : EnemyProjectile
{
    void Start()
    {
        destructionTimer = 20;
        damage = 100;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.right * speed); ;
    }
    
   
    
}
