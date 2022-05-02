using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : EnemyProjectile
{
    private void Awake()
    {
        Selddestruction += Destroy;
    }
    void Start()
    {
        
        destructionTimer = 3;
        damage = 10;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        this.transform.localPosition += new Vector3(direccion * speed * Time.deltaTime, 0, 0);
        
    }
    private void Destroy()
    {
        Debug.Log("el evento fue resivido por" + gameObject.name);
        Selddestruction -= Destroy;
        Destroy(gameObject);
    }
    
    
}
