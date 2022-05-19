using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehavior : MovingCharater
{
    private Animator anim;
    Rigidbody rb;
    public Vector3 stPos;//starting Poscition
    public float Mdist;//movment distans
    public float speed = 6.0f;
    float direction;
    bool onChase, onPatrol;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        stPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity= new Vector3(direction*speed , 0, 0);
        if (onPatrol)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                anim.SetInteger("moving", 1);
            if (transform.position.x < stPos.x - Mdist)
                direction = 1;

            if (transform.position.x > stPos.x + Mdist)
                direction = -1;
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetInteger("moving", 0);
            anim.SetInteger("battle", 1);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                anim.SetInteger("moving", 2);
            float dist = Vector3.Distance(other.transform.position, transform.position);
            onPatrol = false;
            onChase = true;
            if (other.transform.position.x > transform.position.x)
                direction = 1;
            else
                direction = -1;
            if (dist <= 1)
            {
                direction = 0;
            }
                
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetInteger("moving", 0);
            anim.SetInteger("battle", 0);
            onPatrol = true;
            onChase = false;
        }
    }
}
