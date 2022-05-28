using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanger : MonoBehaviour
{
    public static AudioSource goblinGetingHit;

    public static void GoblinGetingHit()
    {
        goblinGetingHit.Play();
    }
    
    
}
