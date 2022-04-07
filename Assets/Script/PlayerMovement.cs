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
    Quaternion faceRight,faceLeft;
    int wallSide;
    public bool touchinWall, isGrounded,walljumping,dashing;
    #endregion

    void Start()
    {
        faceLeft = Quaternion.Euler(0, 90, 0);
        faceRight = Quaternion.Euler(0, 270, 0);
        rb = this.GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        axiX = Input.GetAxis("Horizontal");
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jump);
            this.GetComponent<Animator>().SetBool("jumping", true);
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

        if (!walljumping && !dashing)
        {
            if (axiX != 0)
            {
                this.GetComponent<Animator>().SetBool("moving", true);
                rb.velocity = (new Vector3(-axiX * speed, rb.velocity.y, 0));
                if (axiX == 1 && (this.gameObject.transform.eulerAngles.y >= 270|| this.gameObject.transform.eulerAngles.y <= 100))
                {
                    rotatePJ(-1);
                    Debug.Log(this.gameObject.transform.eulerAngles.y);
                    this.GetComponent<Animator>().SetBool("turningrigth", true);
                    if(this.gameObject.transform.eulerAngles.y <= 270&&this.gameObject.transform.eulerAngles.y>= 100)
                    {
                       this.GetComponent<Animator>().SetBool("turningrigth", false);
                        this.gameObject.transform.rotation = faceRight;
                    }
                }
                if (axiX == -1 && (this.gameObject.transform.eulerAngles.y >= 260 || this.gameObject.transform.eulerAngles.y <= 90))
                {
                    rotatePJ(1);
                    this.GetComponent<Animator>().SetBool("turningleft", true);
                    Debug.Log(this.gameObject.transform.eulerAngles.y);
                    if (this.gameObject.transform.eulerAngles.y <= 260 && this.gameObject.transform.eulerAngles.y >= 90)
                    {
                        this.GetComponent<Animator>().SetBool("turningleft", false);
                        this.gameObject.transform.rotation = faceLeft;
                    }
                }
                //if (axiX == -1 && transform.eulerAngles.y <= 90)
                //    rotatePJ(1);

            }
            else
            {
                this.GetComponent<Animator>().SetBool("moving", false);
            }
        }
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
            this.GetComponent<Animator>().SetBool("jumping", false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if(col.gameObject.CompareTag("Wall"))
        wallSide = CheckWallSide(col);

    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            touchinWall = false;
            this.GetComponent<Animator>().SetBool("onWall", false);
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Wall"))
        { 
            touchinWall = true;
            this.GetComponent<Animator>().SetBool("onWall", true);
        }
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
    void rotatePJ(int direction)
    {
        this.gameObject.transform.Rotate(0, 360 * direction*Time.deltaTime, 0);
    }
}
