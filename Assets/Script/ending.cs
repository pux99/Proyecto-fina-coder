using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{
    public GameObject endscene;
    public Transform camara;
    // Start is called before the first frame update
    void Start()
    {
        neclesOFGlory.GotTheNeckles += activate;
    }
    void activate(string nada)
    {
         this.GetComponent<SphereCollider>().enabled=true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endscene.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
