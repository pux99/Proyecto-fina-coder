using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Varibles
    public float speed, jump, jumpCD, wallJumpX, wallJumpY, wallJumpTimer, dashForce, dashduration, dashCD;
    float dashTimer;
    public Rigidbody rb;
    float axiX;
    int wallSide;
    public bool touchinWall, isGrounded,walljumping,dashing;
    #endregion

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        axiX = Input.GetAxis("Horizontal");
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jump);
            //rb.velocity=(transform.up * jump);
            isGrounded = false;
        }
        if (touchinWall && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            rb.velocity = new Vector3(wallSide * wallJumpX, wallJumpY, 0);
            walljumping = true;
            Invoke(nameof(WallJumpEnd), wallJumpTimer);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)&&dashCD<dashTimer)
        { 
            rb.useGravity = false;
            rb.velocity = new Vector3(-axiX*dashForce, 0, 0);
            dashing = true;
            Invoke(nameof(EndOfDash), dashduration);
            dashTimer = 0;
        }
        if (dashTimer < dashCD)
            dashTimer += Time.deltaTime;
       
    }
    void FixedUpdate()
    {
       
        #region Movment    
        
        if(!walljumping&&!dashing)
        rb.velocity=(new Vector3(-axiX* speed,rb.velocity.y,0));
        #endregion

        
        
        //if (wallSide == "left" && touchinWall && Input.GetKeyDown(KeyCode.Space))
        //    rb.AddForce(new Vector3(+1, 3, 0).normalized * wallJump, ForceMode.Impulse);
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= 0)
        {
            //rb.AddForce(transform.right * -Input.GetAxis("Horizontal") * dashForce, ForceMode.VelocityChange);
            rb.velocity = (transform.right * -Input.GetAxis("Horizontal") * dashForce);
            dashCD = 3;
        }
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (CheckSide(Vector3.up, col)&&col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if(col.gameObject.CompareTag("Wall"))
        wallSide = CheckWallSide(col);

    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Wall"))
            touchinWall = false;
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Wall"))
            touchinWall = true;
    }
    bool CheckSide(Vector3 standarSide,Collision col)
    {
        foreach (ContactPoint hitPoint in col.contacts)
        {
            if (hitPoint.normal == standarSide)
            {
                return true;
            }
        }      
        return false;
    }
    int  CheckWallSide(Collision col)
    {
        foreach (ContactPoint hitPoint in col.contacts)
        {
            if (hitPoint.normal == Vector3.right)
            {
                Debug.Log("rigth");
               return 1;
            }
            else if (hitPoint.normal == -Vector3.right)
                return -1 ;
        }
        return 0;
    }
    void WallJumpEnd()
    {
        walljumping = false;
    }
    void EndOfDash()
    {
        rb.useGravity = true;
        dashing = false;
    }
}
