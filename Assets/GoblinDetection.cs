using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDetection : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("test");
    }
    void OnTriggerStay(Collider Player)
    {        
        if (Player.CompareTag("Player"))
            GetComponentInParent<GoblinScript>().Combat(Player.gameObject);
        
    }
    private void OnTriggerExit(Collider Player)
    {
        if (Player.CompareTag("Player"))
            GetComponentInParent<GoblinScript>().battle_state = false;
    }
}
