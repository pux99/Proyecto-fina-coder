using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyProjectile : MonoBehaviour
{
    public static event Action<float> Damage;
    public static event Action Selddestruction;
    public float damage,speed,destructionTimer;
    public int direccion;
    
    void Start()
    {
        
        Destroy(this.gameObject, destructionTimer);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("el evento fue llamado por" + gameObject.name);
            Damage?.Invoke(damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("el evento fue llamado por" + gameObject.name);
            Damage?.Invoke(damage); 
        }
        Selddestruction?.Invoke();
    }
    [SerializeField] public   float DoDamage()
    {
        return damage;
    }
}
