using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFog : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        transform.gameObject.SetActive(false);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        //if(timer>=2)
          //  transform.gameObject.SetActive(false);

    }
    
    void ActiveParticle()
    {
        Debug.Log("el evento fue resivido por" + gameObject.name);
        GetComponent<ParticleSystem>().Play();
        transform.gameObject.SetActive(true);
    }
}
