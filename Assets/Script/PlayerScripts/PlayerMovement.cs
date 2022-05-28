using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MovingCharater
{
    #region Varibles
    public AudioSource magic;
    public AudioSource getingHit;
    public AudioSource Hiting;
    public AudioSource deadS;
    public Vector3 spawnPoint;
    public float  jump, wallJumpX, wallJumpY, wallJumpTimer, dashForce, dashduration;
    public static float  dashCD, dashTimer,displayLife,displayMana;
    private float proyectileCD =1;
    private float facing = 1;
    public float mana;
    private string angle;
    private bool TurnigBlock;
    public bool touchinWall, isGrounded, walljumping, dashing;
    public int wallSide;
    public CharacterManager charaterManager; 
    public GameObject projectiel;
    private GameObject meleeCol;
    private Quaternion faceRight, faceLeft;
    #endregion

    void Start()
    {
        charaterManager = CharacterManager.instans;
        mana = charaterManager.GetComponent<CharacterManager>().mana;
        life = charaterManager.GetComponent<CharacterManager>().life;
        meleeCol = transform.Find("MeleeHitColider").gameObject;
        Attack.Damage += ResiveDamage;
        PickUpObject.PickUP += PickingUp;
        dashCD = 2f;
        dashTimer = dashCD;
        faceLeft = Quaternion.Euler(0, 90, 0);
        faceRight = Quaternion.Euler(0, 270, 0);
        rb = this.GetComponent<Rigidbody>();

    }
    private void Update()
    {
        displayMana = mana;
        displayLife = life;
        mana= charaterManager.GetComponent<CharacterManager>().mana ;
        life =charaterManager.GetComponent<CharacterManager>().life ;
        if (invultimer>=0)
            invultimer-=Time.deltaTime;
        if(proyectileCD > 0)
            proyectileCD -= Time.deltaTime;
        this.GetComponent<Animator>().SetBool("spell", false);
        if (Input.GetKeyDown(KeyCode.E))
            ThrowProjectile();
        if(life<=0)
            StartCoroutine( Dead());
        if(Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(Melee());
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= dashTimer)
            Dash();
        
        if (dashTimer < dashCD)
            dashTimer += Time.deltaTime;
        
        axiX = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.D) && TurnigBlock==false)
        {
            angle = "left";
            facing = 1;
            TurnigBlock = true;
        }
        else TurnigBlock = false;
        if (Input.GetKey(KeyCode.A)&& TurnigBlock == false)
        { 
            angle = "rigth";
            facing = -1;
            TurnigBlock = true;
        }
        else TurnigBlock = false;
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
        if (CheckSide(Vector3.up, col) && col.gameObject.CompareTag("Ground"))
        {
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
                    charaterManager.GetComponent<CharacterManager>().life -= 10;
                    invultimer = 1f;
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
        
        if (CheckSide(Vector3.up, col) && col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
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
    IEnumerator Dead()
    {
        charaterManager.GetComponent<CharacterManager>().life=100;
        charaterManager.GetComponent<CharacterManager>().mana = 100;
        ColorManager.darken = true;
        sounds(deadS);
        yield return new WaitForSeconds(2);
        transform.position = spawnPoint;
        yield return new WaitForSeconds(1);
        ColorManager.darken = false;
    }
    private IEnumerator Melee()
    {
        meleeCol.SetActive(true);
        this.GetComponent<Animator>().SetBool("attaking", true);
        yield return new WaitForSeconds(.2f);
        sounds(Hiting);
        this.GetComponent<Animator>().SetBool("attaking", false);
        meleeCol.SetActive(false);
    }
    protected override IEnumerator ResiveDamageProperty(GameObject target)
    {
        charaterManager.GetComponent<CharacterManager>().life -=10 ;
        this.GetComponent<Animator>().SetBool("getHit", true);
        rb.velocity = new Vector3(0, 2, 0);
        yield return new WaitForSeconds(0.1f);
        sounds(getingHit);
        this.GetComponent<Animator>().SetBool("getHit", false);
    }
    void ThrowProjectile()
    {
        GameObject disparo; 
        if (proyectileCD <= 0 && mana>=10)
        {
            this.GetComponent<Animator>().SetBool("spell", true);
            sounds(magic);
            disparo = Instantiate(projectiel, transform.position + new Vector3(-facing * .5f, 1, 0), Quaternion.identity);
            disparo.GetComponent<PlayerProyectile>().direccion = -facing;
            charaterManager.GetComponent<CharacterManager>().mana -= 10;
            proyectileCD = .5f;
        }
    }
    void sounds(AudioSource sound)
    {
        sound.Play();
    }
    public void PickingUp(String type,float value)
    {
        switch (type)
        {
            case "Life":
                charaterManager.GetComponent<CharacterManager>().life += value;
                break;
            case "Mana":
                charaterManager.GetComponent<CharacterManager>().mana += value;
                break;
        }
    }
    public void ResetValues()
    {
        charaterManager.GetComponent<CharacterManager>().life = 100;
        charaterManager.GetComponent<CharacterManager>().mana = 100;
    }
}
