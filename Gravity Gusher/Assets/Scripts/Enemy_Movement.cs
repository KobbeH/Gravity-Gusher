using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

	Transform dragon;
	public float speedX;
	public float speedY;
	public bool goingRight;
	public bool goingUp;
	public bool Horizontal;
	public bool Vertical;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	public Game_Manager manager;
	bool alive;
	float timeIdle = 0;
	Animator anim;
	AudioSource sound;
	[SerializeField] private GameObject blueFirePrefab;
	private Rigidbody2D rb2D;

	void Start()
	{
		alive = true;
		anim = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
		manager.addEnemy();
		speedX = Random.Range(5f, 10f);	//chooses random Horizontal speed at start of program
		speedY = Random.Range(1f, 5f);	//chooses random Vertical speed at start of program
		dragon = this.transform;
		goingRight = true;	//starts dragon movement going right
		Horizontal = true;	//automatically starts the horizontal movement
		rb2D = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Update()
	{
		if (Horizontal && goingRight && alive)	//if moving right and alive then use the random speedX variable
		{
			rb2D.velocity = new Vector3(speedX, rb2D.velocity.y, 0);
		}
		else if (Horizontal &! goingRight && alive)	//if moving left and alive then use negative random speedX variable
			{
			rb2D.velocity = new Vector3(-speedX, rb2D.velocity.y, 0);
		}
		if (Vertical && goingUp && alive)	//if moving up  and alive then use random speedY variable
		{
			rb2D.velocity = new Vector3(rb2D.velocity.x, speedY, 0);
		}
		else if (Vertical & !goingUp && alive)	//if moving down and alive then use negative random speedY variable
		{
			rb2D.velocity = new Vector3(rb2D.velocity.x, -speedY, 0);
		}

		if (dragon.position.x <= minX)	//if dragon is at or to the left of the minimum X
		{
			if (!goingRight)	//if dragon is moving left
			{
				goingRight = true;	//then make dragon go to right
				dragon.RotateAround(dragon.position, dragon.up, 180f);	//also make dragon rotate to face the right
			}
		}
		if (dragon.position.x >= maxX)	//if dragon is at or to the right of maximun x
		{
			if (goingRight)	//if dragon is moving right
			{
				goingRight = false;	//then make dragon go to left
				dragon.RotateAround(dragon.position, dragon.up, 180f);	//also make dragon rotate to face left
			}
		}

		if (dragon.position.y <= minY)	//if dragon is at or bellow minimum Y
		{
			if (!goingUp)	//if dragon is going up down
			{
				goingUp = true;	//then make dragon go up
			}
		}
		if (dragon.position.y >= maxY)	//if dragon is at or above maximum Y
		{
			if (goingUp)	//if dragon is going up
			{
				goingUp = false;	//then make dragon go down
			}
		}
		
		// Use this for initialization

		//Start timer if dragon is dead
		if (!alive)
		{
			timeIdle += Time.deltaTime;
		}
		//After 'x' seconds instantiate the blue fire particles, tell manager an enemy has died, and then destroy the dragon
		if (timeIdle > 1.8)
		{
			Instantiate(blueFirePrefab, transform.position, Quaternion.identity);
			manager.subtractEnemy();
			manager.addScore(1500);
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
		{
			Dragon dragon = other.collider.GetComponent<Dragon>();

			//If the object that collided was a dragon, kill the enermy dragon
			if (dragon != null)
			{
				if (alive)	//if alive
				{
					die();	//then die
				}

				return;
			}
		//Check if hit by fire attack
		if (other.collider.GetComponent<FlamePowerup>() != null)
		{
			if (alive)
			{
				die();
			}
		}

		//Since it's not a dragon or boundary, the colliding object must be an obstacle.
		//If the obstacle hits the dragon within a certain radius then kill the dragon
		if (other.contacts[0].normal.y < -0.5)
			{
				if (alive)	//if alive
				{
					die();	//then die
				}
				return;
			}

		}
		private void die()
		{
			//Called when Dragon dies. Plays death sound and death animation and set alive = false. Also sets gravity scale so the dragon falls before dying
			sound.Play();
			anim.enabled = false;
			alive = false;
			GetComponent<Rigidbody2D>().gravityScale = 3f;
		}
}