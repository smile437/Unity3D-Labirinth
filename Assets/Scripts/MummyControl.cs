using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyControl : EnemyControll 
{
	private float maxSpeed = 6f; 
	private float move = 1f;
	private Animator anim;

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
		GetComponent<Rigidbody> ().velocity = new Vector3 (speed, GetComponent<Rigidbody> ().velocity.y, GetComponent<Rigidbody> ().velocity.z);

		if (this.move != 0)
		{
			anim.Play ("MummyWalkAnimation");
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
			anim.Play ("MummyAttackAnimation");
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
