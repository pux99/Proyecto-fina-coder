using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProyectile : EnemyProjectile
{
    void Start()
    {
        
        destructionTimer = 3;
        Destroy(gameObject, destructionTimer);
    }
    
    // Update is called once per frame
    void Update()
    { 
        this.transform.localPosition += new Vector3(direccion * speed * Time.deltaTime, 0, 0);
        
    }
    protected override void attackPropetis(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
