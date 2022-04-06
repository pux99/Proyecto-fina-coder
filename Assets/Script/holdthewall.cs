using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdthewall : MonoBehaviour
{
    public bool touchinWall;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            touchinWall = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            touchinWall = false;
    }
    
}
