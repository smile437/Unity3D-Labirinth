using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControll : EnemyControll 
{
	private float maxSpeed = 3f; 
	private float move = 1f;
	private Animator anim;

	public EnemyDirection CurrDirection;
	public float Multiply = 1.0f;
	public GameObject Target;

	void Start () 
	{
		this.anim = GetComponent<Animator>();
	}

	void Update () 
	{
		var speed = this.move * this.maxSpeed;
		speed *= this.Multiply;
		switch(this.CurrDirection)
		{
		case EnemyDirection.Vertical:
			GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, GetComponent<Rigidbody> ().velocity.y, speed);
			break;
		case EnemyDirection.Horizontal:
			GetComponent<Rigidbody> ().velocity = new Vector3 (speed, GetComponent<Rigidbody> ().velocity.y, GetComponent<Rigidbody> ().velocity.z);
			break;
		}
			
		if (this.move != 0)
		{
			anim.Play ("ZombieWalkAnimation");
		}
		if (this.move > 0 && !this.isFacingRight)
		{
			this.Flip ();
		}
		else if (this.move < 0 && this.isFacingRight) 
		{
			this.Flip ();
		}

		if (LevelController.CoinsCount > 20)
		{
			
		}
	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.gameObject.tag) 
		{
		case "Wall":
			this.move = -this.move;
			break;
		case "Player":
			anim.Play ("ZombieAttackAnimation");
			break;

		}


	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Wall") 
		{
			GetComponent<BoxCollider> ().isTrigger = true;
		}
	}


}
