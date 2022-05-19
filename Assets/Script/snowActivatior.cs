using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowActivatior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
    }
    public void Snowactivate()
    {
        Debug.Log("el evento fue resivido por" + gameObject.name);
        GetComponent<ParticleSystem>().Play();
    }
}
