using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovment : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(1, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.position = transform.position;
        //Destroy(gameObject);
    }
}
