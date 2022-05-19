using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    [SerializeField] private UnityEvent grow, shrink,nevar;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("el evento fue llamado por" + gameObject.name);
            shrink.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("el evento fue llamado por" + gameObject.name);
            grow.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("el evento fue llamado por" + gameObject.name);
            nevar.Invoke();
        }
    }
}
