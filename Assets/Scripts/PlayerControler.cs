using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

	private float maxSpeed = 3f; 
	private bool isFacingRight = true;
	private Animator anim;


	void Start () 
	{
		this.anim = GetComponent<Animator>();
		Time.timeScale = 1;
	}
	
	void Update()
	{
		var moveV = Input.GetAxis ("Vertical");
		var moveH = Input.GetAxis ("Horizontal");

		GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, moveV * maxSpeed);
		GetComponent<Rigidbody>().velocity = new Vector3 (moveH * maxSpeed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);

	
		if (moveH != 0 || moveV != 0)
		{
			anim.Play ("PlayerWalkAnimation");
		}
		else 
		{
			anim.Play ("PlayerIdleAnimation");
		}

		if (moveH > 0 && !isFacingRight)
		{
			Flip ();
		}
		else if (moveH < 0 && isFacingRight) 
		{
			Flip ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Coin")
		{
			Destroy (other.gameObject);
			LevelController.CoinsCount++;
			LevelController.CoinsOnLevel--;
			LevelController.CoinPicked ();
			MazeGen.GoalsCount--;
		} 
		else if (other.gameObject.tag == "Wall") 
		{
			GetComponent<BoxCollider> ().isTrigger = false;
		}
		else if(other.gameObject.tag == "Enemy")
		{
			if(other.gameObject.name == "Mummy")
			{
				LevelController.CoinsCount = 0;
			}

			LevelController.exitReason = "dead";
			Time.timeScale = 0;
			LevelController.KillPlayer ();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Wall") 
		{
			GetComponent<BoxCollider> ().isTrigger = true;
		}
	}

	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
