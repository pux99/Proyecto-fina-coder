using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Varibles
    public Vector3 spawnPoint;
    public GameObject smock,test;
    public float speed, jump, jumpCD, wallJumpX, wallJumpY, wallJumpTimer, dashForce, dashduration, dashCD, ultimaDireccion,life;
    float dashTimer;
    public Rigidbody rb;
    float axiX;
    Quaternion faceRight,faceLeft;
    int wallSide;
    public bool touchinWall, isGrounded,walljumping,dashing;
    #endregion

    void Start()
    {
        life = 100;
        dashTimer = dashCD;
        faceLeft = Quaternion.Euler(0, 90, 0);
        faceRight = Quaternion.Euler(0, 270, 0);
        rb = this.GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        if(life<=0)
        {
            Dead();
        }
        if((gameObject.transform.eulerAngles.y!=90&& gameObject.transform.eulerAngles.y != 270)||axiX!=0)
        rotatePJ(ultimaDireccion);
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
        if (Input.GetKeyDown(KeyCode.LeftShift)&&dashCD<=dashTimer)
        {
            test =Instantiate(smock, transform.position, Quaternion.identity, this.transform);
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
                
                if (axiX == 1 && gameObject.transform.eulerAngles.y!=270)
                {
                    ultimaDireccion = 1;
                    this.GetComponent<Animator>().SetBool("turningrigth", true);
                }
                if (axiX == -1 && gameObject.transform.eulerAngles.y != 90)
                {
                    ultimaDireccion = -1;
                    this.GetComponent<Animator>().SetBool("turningleft", true);
                }


            }
            else
            {
                this.GetComponent<Animator>().SetBool("moving", false);
            }
        }
        #endregion        
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Respawn"))
        {
            spawnPoint = other.gameObject.transform.position;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            life -= 10;
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
        Destroy(test,0.05f);
        rb.useGravity = true;
        dashing = false;
    }
    void rotatePJ(float direction)
    {   
        float rotacion = gameObject.transform.eulerAngles.y;
        if((rotacion<90&&rotacion>-1)||(rotacion<360&&rotacion>270)||(rotacion==90&&direction>=1)|| (rotacion == 270 && direction <= -1))
        gameObject.transform.Rotate(0, 360 * -direction*Time.deltaTime, 0);
        if (rotacion > 90 && rotacion <= 120)
        {
            gameObject.transform.rotation = faceLeft;
            this.GetComponent<Animator>().SetBool("turningleft", false);
        }
        if (rotacion < 270 && rotacion >= 250)
        {
            gameObject.transform.rotation = faceRight;
            this.GetComponent<Animator>().SetBool("turningrigth", false);
            
        }
    }
    void Dead()
    {
        transform.position = spawnPoint;
        life = 100;
    }
}
