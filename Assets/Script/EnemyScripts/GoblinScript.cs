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
	public bool Atacking;
	private bool atackdelay;
	private bool dead;
	private bool hit;
	private int deadRandomizer;
	private Vector3 orgPos;
	[HideInInspector]
	public GameObject meleeCol;
	public GameObject magicAtk;
	public GameObject drops;
    #endregion
    void Start()
	{
		angle = "rigth";
		Atacking = false;
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

				if (!battle_state && canPatrol&&!Atacking&&!hit)
					Patroling(orgPos.x + mdist, orgPos.x - mdist);
				if (!canPatrol)
				{
					Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back), Color.green, 2, false);
					if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out RaycastHit hit, 5)&& hit.transform.gameObject.CompareTag("Player"))
					{
						if (angle == "left")
							angle = "rigth";
						else angle = "left";
						Turning(angle);
                    }
				}
				
				Turning(angle);

				if (direction != 0)
				{
					if (battle_state == false)
						runSpeed = 1.0f;
					else
						runSpeed = 1.5f;
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
		if(!hit)
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
		if (!hit)
		{
			anim.SetInteger("battle", 1);

			if (!Atacking)
				anim.SetInteger("moving", 2);
			runSpeed = 1.8f;
			battle_state = true;
			dist = transform.position.x - Player.transform.position.x;
			if (transform.position.x < Player.transform.position.x)
			{
				direction = 1;
				angle = "rigth";
			}
			else
			{
				direction = -1;
				angle = "left";
			}
			if (dist <= 1.2 && dist >= -1.2 && !atackdelay)
			{
				StartCoroutine(Melee());
				Debug.Log("pegue");
			}
			if (dist <= 1f && dist >= -1f)
			{
				Atacking = true;
				if (!hit)
					anim.SetInteger("moving", 0);
			}
			else if (!atackdelay) Atacking = false;
		}
	}

	private IEnumerator Melee()
    {
		Atacking = true;
		atackdelay = true;
		anim.SetInteger("moving", 0);

		yield return new WaitForSeconds(0.1f);
		anim.SetInteger("moving", 3);
		yield return new WaitForSeconds(0.5f);
		meleeCol.SetActive(true);
		Sounds(Hiting);
		yield return new WaitForSeconds(.5f);
		meleeCol.SetActive(false);
		anim.SetInteger("moving", 0);
		Atacking = false;
		yield return new WaitForSeconds(2);
		atackdelay = false;
	}

	public void MagicAtack(GameObject Player)
    {
        #region varibles
        Vector3 normal;
		#endregion
		if (!Atacking)
		{
			anim.SetInteger("battle", 1);
			normal = (transform.position - Player.transform.position).normalized;
			spellCDCounter -= Time.deltaTime;

			if (spellCDCounter <= 0)
			{
				Atacking = true;
				spellCDCounter = spellCD;
				anim.SetInteger("moving", 5);
				StartCoroutine(FireProyectile(normal));

			}
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
        if (!dead)
        {
			Sounds(Hiting);
			disparo = Instantiate(magicAtk, transform.position + new Vector3(direction * .5f, 1, 0), Quaternion.identity);
			disparo.GetComponent<EnemyProjectile>().direccion = direction;
			disparo.GetComponent<Fireball>().normal = normal;
			anim.SetInteger("moving", 0);

		}
		Atacking = false;

	}
	protected override IEnumerator ResiveDamageProperty(GameObject target)
    {
		if (life > 0)
		{
			switch (type)
			{
				case GoblinType.chaman:
					anim.SetInteger("moving", 16);
					break;
				case GoblinType.rouge:
					anim.SetInteger("moving", 10);
					break;
				case GoblinType.warrior:
					anim.SetInteger("moving", 15);
					break;
			}
			Sounds(getingHit);
		}
		hit = true;
		yield return new WaitForSeconds(0.1f);
		anim.SetInteger("moving", 0);
		yield return new WaitForSeconds(0.4f);
		hit = false;
	}
	protected void Dead(int typeOfDead)
	{
		int dropChance, droptype;
		dead = true;
		switch (type)
		{
			case GoblinType.chaman:
				anim.SetInteger("moving", typeOfDead + 1);
				break;
			case GoblinType.rouge:
				anim.SetInteger("moving", typeOfDead);
				break;
			case GoblinType.warrior:
				anim.SetInteger("moving", typeOfDead+1);
				break;
		}
		StopAllCoroutines();
		dropChance = UnityEngine.Random.Range(0, 100);
		droptype = UnityEngine.Random.Range(0, 2);
		if(dropChance>50)
			Instantiate(drops.transform.GetChild(droptype), transform.position + new Vector3(0, 1, 0), Quaternion.identity);
		Sounds(deadS);
		Destroy(this.gameObject, 2);
	}
	void Sounds(AudioSource sound)
    {
		sound.Play();
    }
}

