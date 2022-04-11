using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinLog : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    void Start()
    {
        Destroy(this.gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.right * speed); ;
    }
    /*private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            this.gameObject.GetComponent<Collider>().isTrigger = false;
    }
    */
}
