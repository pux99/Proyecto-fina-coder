using UnityEngine;
using System.Collections;
using System;


public class GoblinScript : MovingCharater
{
	#region variables
	public AudioSource getingHit;
	public AudioSource Hiting;
	public AudioSource deadS;
	public enum GoblinType {warrior,rouge,chaman,archer};
	public GoblinType type;
	public bool canPatrol;
	private Animator anim;
	private CharacterController controller;
	[HideInInspector]
	public bool battle_state;
	public float runSpeed = 1.7f;
	public float turnSpeed = 60.0f;
	public float gravity = 20.0f;
	public float mdist;
	private float direction=1;
	public float spellCD;
	private float spellCDCounter;
	private String angle;
	private bool Atacking;
	private bool dead;
	private bool hit;
	private int deadRandomizer;
	private Vector3 orgPos;
	[HideInInspector]
	public GameObject meleeCol;
	public GameObject magicAtk;
    #endregion
    void Start()
	{
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		meleeCol = transform.Find("MeleeHitColider").gameObject;
		orgPos = transform.position;
		deadRandomizer = UnityEngine.Random.Range(12, 13);
		Attack.Damage += ResiveDamage;
		anim.SetInteger("moving", 0);
	}
	
	void Update () 
	{
		
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);//unamobible en eje z
		if (!hit)
		{
			if (life > 0)
			{
				if (invultimer > 0)
					invultimer -= Time.deltaTime;

				if (!battle_state && canPatrol)
					Patroling(orgPos.x + mdist, orgPos.x - mdist);

				Turning(angle);

				if (direction != 0)
				{
					if (battle_state == false)
						runSpeed = 1.0f;
					else
						runSpeed = 2.6f;
				}

				if (!Atacking && canPatrol)
					Moving(direction);
			}
			else
			{
				if (!dead)
					Dead(deadRandomizer);
				else
					anim.SetInteger("moving", 0);
			}
		}
	}
	
	void Patroling(float rigthEnd,float leftEnd)
    {
		anim.SetInteger("battle", 0);
		anim.SetInteger("moving", 1);

		if (transform.position.x > rigthEnd)
		{ 
			direction = -1;
			angle = "left";
		}

		if (transform.position.x < leftEnd)
		{
			direction = 1;
			angle = "rigth";
		}
	}

	protected override void Moving(float direction)
    {
		controller.SimpleMove(new Vector3(direction * speed * runSpeed, 0, 0));
	}

	public void Combat(GameObject Player)
    {
		float dist;

		anim.SetInteger("battle", 1);
		
		if (!Atacking)
		anim.SetInteger("moving", 2);
		runSpeed = 2.6f;
		battle_state = true;
		dist=transform.position.x - Player.transform.position.x;
		if (transform.position.x < Player.transform.position.x)
		{
			direction = 1;
			angle = "rigth";
		}else
		{
			direction = -1;
			angle = "left";
		}
		if (dist <= 1 && dist>=-1 &&!Atacking)
		{
			StartCoroutine(Melee());
		}
	}

	private IEnumerator Melee()
    {
		Atacking = true;
		anim.SetInteger("moving", 0);

		yield return new WaitForSeconds(0.1f);
		anim.SetInteger("moving", 3);
		meleeCol.SetActive(true);

		yield return new WaitForSeconds(.5f); 
		meleeCol.SetActive(false);
		Atacking = false;
	}

	public void MagicAtack(GameObject Player)
    {
        #region varibles
        Vector3 normal;
		#endregion
		anim.SetInteger("battle", 1);
		normal=(transform.position - Player.transform.position).normalized;
		spellCDCounter -= Time.deltaTime;

		if (spellCDCounter <= 0)
		{
			Atacking = true;
			spellCDCounter = spellCD;
			anim.SetInteger("moving", 5);
			StartCoroutine( FireProyectile(normal));

		}
		
	}
	public void ChangeAnimationState(string state,int value)
    {
		anim.SetInteger(state,value);
	}
	IEnumerator FireProyectile(Vector3 normal)
    {
		GameObject disparo;
		yield return new WaitForSeconds(1);
		sounds(Hiting);
		disparo = Instantiate(magicAtk, transform.position + new Vector3(direction * .5f, 1, 0), Quaternion.identity);
		disparo.GetComponent<EnemyProjectile>().direccion = direction;
		disparo.GetComponent<Fireball>().normal = normal;
		anim.SetInteger("moving", 0);

	}
	protected override IEnumerator ResiveDamageProperty(GameObject target)
    {
		switch(type)
        {
			case GoblinType.chaman:
				anim.SetInteger("moving", 16);
				break;
			case GoblinType.rouge:
				anim.SetInteger("moving", 10);
				break;
			case GoblinType.warrior:
				anim.SetInteger("moving", 10);
				break;
		}
		hit = true;
		if (life > 0)
			sounds(getingHit);
		yield return new WaitForSeconds(0.1f);
		anim.SetInteger("moving", 0);
		yield return new WaitForSeconds(0.4f);
		hit = false;
	}
	protected void Dead(int typeOfDead)
	{
		switch (type)
		{
			case GoblinType.chaman:
				anim.SetInteger("moving", typeOfDead + 1);
				break;
			case GoblinType.rouge:
				anim.SetInteger("moving", typeOfDead);
				break;
			case GoblinType.warrior:
				anim.SetInteger("moving", typeOfDead);
				break;
		}
		sounds(deadS);
		Destroy(this.gameObject, 2);
		dead = true;
	}
	void sounds(AudioSource sound)
    {
		sound.Play();
    }
}

