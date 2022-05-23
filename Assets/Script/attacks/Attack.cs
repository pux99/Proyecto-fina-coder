using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attack : MonoBehaviour
{
    public float damage;
    public static event Action<float, GameObject> Damage;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Damage?.Invoke(damage, other.gameObject);
            attackPropetis(other);
        }
        if (other.CompareTag("Player"))
        {
            Damage?.Invoke(damage, other.gameObject);
            attackPropetis(other);
        }
        


    }
    protected virtual void attackPropetis(Collider col)
    {

    }
    
}
