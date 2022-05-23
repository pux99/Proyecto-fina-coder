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
    }
    void Detectar ()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hit, 10000000))
        {
            if (hit.transform.CompareTag("Player"))
            {
                timer += Time.deltaTime;
                if (timer >= CD)
                { 
                    Instantiate(Proyectile, transform.position, quat, transform);
                    timer = 0;
                }
            }
        }
    }
}
