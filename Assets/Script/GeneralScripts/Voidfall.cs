using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidfall : MonoBehaviour
{
    public CharacterManager charaterManager;
    void Start()
    {
        charaterManager = CharacterManager.instans;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            charaterManager.life = 0;
        }
    }

}
