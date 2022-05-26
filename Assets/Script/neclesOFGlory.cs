using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class neclesOFGlory : MonoBehaviour
{
    public GameObject fallingobjects;
    public static event Action<string> GotTheNeckles;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            fallingobjects.SetActive(true);
            CamaraShake.Instance.ShakeCamera(2);
            GotTheNeckles?.Invoke("Escapa de la cueva");
            Destroy(gameObject);
        }
    }
}
