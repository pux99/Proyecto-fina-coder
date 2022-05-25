using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUpObject : MonoBehaviour
{
    public static event Action<String,float> PickUP;
    public String type;
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PickUP?.Invoke(type, value);
            Destroy(gameObject);
        }
            
    }
}
