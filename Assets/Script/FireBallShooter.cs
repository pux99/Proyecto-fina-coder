using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallShooter : MonoBehaviour
{
    public GameObject fireball;
    float CD;
    void Update()
    {
        detectar();
    }
    void detectar ()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.right),out hit,10000000))
        {
            if (hit.transform.CompareTag("Player"))
            {
                CD += Time.deltaTime;
                if(CD>=2)
                { 
                    Instantiate(fireball, transform.position, Quaternion.identity,this.transform);
                    CD = 0;
                }
            }
        }
    }
}
