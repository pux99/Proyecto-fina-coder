using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileShooter : MonoBehaviour
{
    public Vector3 rotation;
    Quaternion quat;
    public GameObject Proyectile;
    public float CD,timer;
    void Update()
    {
        quat = Quaternion.Euler(rotation);
        Detectar();
        timer += Time.deltaTime;
    }
    void Detectar ()
    {
        GameObject fireball;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hit, 10000000))
        {
            if (hit.transform.CompareTag("Player"))
            {
                
                if (timer >= CD)
                { 
                    fireball=Instantiate(Proyectile, transform.position, quat, transform);
                    fireball.GetComponent<Fireball>().normal = new Vector3(-1,0,0);
                    timer = 0;
                }
            }
        }
    }
}
