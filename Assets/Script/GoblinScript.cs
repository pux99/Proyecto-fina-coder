using UnityEngine;
using System.Collections;

public class GoblinScript : MovingCharater
{
	
	
	private Animator anim;
	private CharacterController controller;
	public bool battle_state;
	public float speed = 6.0f;
	public float runSpeed = 1.7f;
	public float turnSpeed = 60.0f;
	public float gravity = 20.0f;
	public float mdist;
	public float direction=1;
	public float angle;
	public bool Atacking;
	private Vector3 moveDirection = Vector3.zero;
	public Vector3 orgPos;
	public GameObject meleeCol;
	
	
	
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		meleeCol = transform.Find("MeleeHitColider").gameObject;
		orgPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!battle_state)
		Patroling(orgPos.x+mdist,orgPos.x-mdist);
		Turning(angle);

		if (Input.GetKey(KeyCode.Alpha2)) //battle_idle
		{
			anim.SetInteger("battle", 1);
			battle_state = true;
			
		}
		if (Input.GetKey(KeyCode.Alpha1)) 			//idle
		{
			anim.SetInteger("battle", 0);
			battle_state = false;
		}
		if (direction != 0) {		 //moving
			if (battle_state == false)
			{
				runSpeed = 1.0f;
			} else 
			{
				runSpeed = 2.6f;
			}
		}
	if(controller.isGrounded)
		{
			moveDirection=transform.right * direction * speed * runSpeed;
		}
		controller.SimpleMove(new Vector3(direction * speed * runSpeed, 0, 0));
	}
	void Patroling(float rigthEnd,float leftEnd)
    {
		anim.SetInteger("battle", 0);
		anim.SetInteger("moving", 1);
		if (transform.position.x > rigthEnd)
		{ 
			direction = -1;
			angle = 270;
		}

		if (transform.position.x < leftEnd)
		{
			direction = 1;
			angle = 90;
		}
	}
	public void Combat(GameObject Player)
    {
		anim.SetInteger("battle", 1);
		if (!Atacking)
		anim.SetInteger("moving", 2);
		runSpeed = 2.6f;
		float dist;
		AtackCD();
		battle_state = true;
		dist=transform.position.x - Player.transform.position.x;
		if (transform.position.x < Player.transform.position.x)
		{
			direction = 1;
			angle = 90;
		}
		else
		{
			direction = -1;
			angle = 270;
		}
		if (dist <= 2 && dist>=-2 &&!Atacking)
		{
			//AtackTimer = 2;
			Debug.Log(dist);
			StartCoroutine(melee());
		}
	}
	void AtackCD()
    {
		//AtackTimer -= Time.deltaTime;
    }
	private IEnumerator melee()
    {
		anim.SetInteger("moving", 0);
		yield return new WaitForSeconds(0.5f);
		anim.SetInteger("moving", 3);
		Atacking = true;
		meleeCol.SetActive(true);
		yield return new WaitForSeconds(1); 
		meleeCol.SetActive(false);
		Atacking = false;
	}
}

