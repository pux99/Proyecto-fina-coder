using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Charaters : MonoBehaviour
{
    public float maxLife, life;
    
    [HideInInspector]
    public Rigidbody rb;
    public float invultimer;

    protected void ResiveHeal(float heal)
    {
        life += heal;
    }
    protected void ResiveDamage(float damage, GameObject target)
    {
        if (target == null||this==null )
            return;
        if(gameObject == target)
        { 
            if (invultimer <= 0)
            {
            life -= damage;
            invultimer = 1f;
                StartCoroutine( ResiveDamageProperty(target));
            }
        }
            
    }
    protected virtual IEnumerator ResiveDamageProperty(GameObject col)
    {
        yield return new WaitForSeconds(0.5f);
    }
}
