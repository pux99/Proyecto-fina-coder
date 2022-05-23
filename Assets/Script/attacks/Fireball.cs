using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : EnemyProjectile
{
    public Vector3 normal;
    void Start()
    {
        destructionTimer = 3;
        damage = 10;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        this.transform.localPosition += new Vector3(-normal.x * speed * Time.deltaTime, -normal.y* speed * Time.deltaTime, 0);
    }
    protected override void attackPropetis(Collider col)
    {
        if(col.CompareTag("Player"))
            Destroy(gameObject);
    }




}
