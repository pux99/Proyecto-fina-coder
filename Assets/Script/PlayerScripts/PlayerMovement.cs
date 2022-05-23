using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MovingCharater
{
    #region Varibles
    public Vector3 spawnPoint;
    public float  jump, wallJumpX, wallJumpY, wallJumpTimer, dashForce, dashduration;
    public static float  dashCD, dashTimer,displayLife;
    float proyectileCD=1;
    string angle;
    float facing=1;
    Quaternion faceRight,faceLeft;
    public int wallSide;
    public GameObject smock, projectiel;
    public bool touchinWall, isGrounded,walljumping,dashing;
    GameObject meleeCol;
    #endregion

    #region Events
    public static event Action ActiveFog;
    #endregion

    void Start()
    {
        life = 100;
        meleeCol = transform.Find("MeleeHitColider").gameObject;
        Attack.Damage += ResiveDamage;
        life = 100;
        dashCD = 2f;
        dashTimer = dashCD;
        faceLeft = Quaternion.Euler(0, 90, 0);
        faceRight = Quaternion.Euler(0, 270, 0);
        rb = this.GetComponent<Rigidbody>();

    }
    private void Update()
    {
        displayLife = life;
        if (invultimer>=0)
            invultimer-=Time.deltaTime;
        if(proyectileCD > 0)
            proyectileCD -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))
            ThrowProjectile();
        if(life<=0)
            Dead();
        if(Input.GetKeyDown(KeyCode.X))
            StartCoroutine(Melee());
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= dashTimer)
            Dash();
        
        if (dashTimer < dashCD)
            dashTimer += Time.deltaTime;
        

        axiX = Input.GetAxis("Horizontal");
        if (axiX == 1)
        {
            angle = "left";
            facing = 1;
        }
        if (axiX == -1)
        { 
            angle = "rigth";
            facing = -1;
        }
        Turning(angle);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jump);
            this.GetComponent<Animator>().SetBool("jumping", true);
        }

        if (touchinWall && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            rb.velocity = new Vector3(wallSide * wallJumpX, wallJumpY, 0);
            walljumping = true;
            Invoke(nameof(WallJumpEnd), wallJumpTimer);
        }
    }
    void FixedUpdate()
    {

        #region Movment    

        if (!walljumping && !dashing)
        {
            if (axiX != 0)
            {
                this.GetComponent<Animator>().SetBool("moving", true);
                Moving(-axiX);
            }
            else
                this.GetComponent<Animator>().SetBool("moving", false);
        }
        #endregion        
    }
    private void Dash()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(-axiX * dashForce, 0, 0);
        dashing = true;
        Invoke(nameof(EndOfDash), dashduration);
        dashTimer = 0;
    }
    private void OnCollisionEnter(Collision col)
    {
        Vector3 InpactDirection;
        if (CheckSide(Vector3.up, col)&&col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            this.GetComponent<Animator>().SetBool("jumping", false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        switch (col.gameObject.tag)
        {
            case "Wall":
                wallSide = CheckWallSide(col);
                break;
            case "Enemy":
                if (invultimer <= 0)
                {
                    Debug.Log("au");
                    InpactDirection = (transform.position - col.transform.position).normalized;
                    rb.AddForce(new Vector3(-1000*InpactDirection.x, 10*InpactDirection.y, 0));
                    life -= 10;
                    invultimer = 1;
                }
                break;
            default:
                break;
        }
        
    }
    private void OnCollisionExit(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Wall":
                touchinWall = false;
                this.GetComponent<Animator>().SetBool("onWall", false);
                break;
            case "Ground":
                isGrounded = false;
                this.GetComponent<Animator>().SetBool("jumping", true);
                break;
            default:
                break;
        }
    }
    private void OnCollisionStay(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Wall":
                touchinWall = true;
                this.GetComponent<Animator>().SetBool("onWall", true);
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Respawn":
                spawnPoint = other.gameObject.transform.position;
                break;
            default:
                break;
        }
    }
    bool CheckSide(Vector3 standarSide,Collision col)
    {
        foreach (ContactPoint hitPoint in col.contacts)
        {
            if (hitPoint.normal == standarSide)
                return true;
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
    void Dead()
    {
        transform.position = spawnPoint;
        ActiveFog?.Invoke();
        Debug.Log("el evento fue llamado por" + gameObject.name);
        life = 100;
    }
    private IEnumerator Melee()
    {
        meleeCol.SetActive(true);
        yield return new WaitForSeconds(1);
        meleeCol.SetActive(false);
    }
    void ThrowProjectile()
    {
        GameObject disparo; 
        if (proyectileCD <= 0)
        {
            disparo = Instantiate(projectiel, transform.position + new Vector3(-facing * .5f, 1, 0), Quaternion.identity);
            disparo.GetComponent<PlayerProyectile>().direccion = -facing;
            proyectileCD = .5f;
        }
    }
}