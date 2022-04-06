using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltar : MonoBehaviour
{
    public bool isGrounded;
    public float jump,wallJump,dashCD,dashForce;
    public Rigidbody rb;
    public GameObject leftArm, rigthArm;
    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jump);
            //rb.velocity=(transform.up * jump);
            isGrounded = false;
        }
        if(leftArm.GetComponent<holdthewall>().touchinWall && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(new Vector3(-1,3,0).normalized * wallJump, ForceMode.Impulse);

        if (rigthArm.GetComponent<holdthewall>().touchinWall && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(new Vector3(+1, 3, 0).normalized * wallJump, ForceMode.Impulse);

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= 0)
        {
            //rb.AddForce(transform.right * -Input.GetAxis("Horizontal") * dashForce, ForceMode.VelocityChange);
            rb.velocity = (transform.right * -Input.GetAxis("Horizontal") * dashForce);
            dashCD = 3;
        }
        else if (dashCD > 0)
            dashCD -= Time.deltaTime;
            

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log(rb.angularVelocity);
            Debug.Log(rb.velocity+"velo");
        }

    }
}
