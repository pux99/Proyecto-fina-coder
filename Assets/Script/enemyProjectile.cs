using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public float damage,speed,destructionTimer;
    public int direccion;
    
    void Start()
    {
        Destroy(this.gameObject, destructionTimer);
    }
    [SerializeField] public   float doDamage()
    {
        return damage;
    }
}
