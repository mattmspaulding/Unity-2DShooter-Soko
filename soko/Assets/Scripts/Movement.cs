using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	private float xInput, yInput;
	public int speed = 6;
	private Rigidbody2D body;
	public bool isMoving;
	public Joystick joystick;

	Animator anim;

	void Start () 
	{
		body = GetComponent<Rigidbody2D>();
		
		anim = GetComponent<Animator>();

		isMoving = false;
	}
	
	void Update ()
	{
		//xInput = Input.GetAxisRaw("Horizontal");
		//yInput = Input.GetAxisRaw("Vertical");
	
		// Regulate joystick input so player movement speed is always the same.
		if(joystick.Horizontal >= .1f)
		{
			xInput = 1;
		} else if(joystick.Horizontal <= -.1f)
		{
			xInput = -1;
		} else
		{
			xInput = 0;
		}

		if(joystick.Vertical >= .1f)
		{
			yInput = 1;
		} else if(joystick.Vertical <= -.1f)
		{
			yInput = -1;
		} else
		{
			yInput = 0;
		}

		isMoving = (xInput != 0 || yInput != 0);

		anim.SetBool("moving", isMoving);

		if(isMoving)
		{
			var moveVector = new Vector2(xInput, yInput);
			body.MovePosition(new Vector2((transform.position.x + moveVector.x * speed * Time.deltaTime),
				transform.position.y + moveVector.y * speed * Time.deltaTime));	
		}	
	
		// Restrict player movement.

		// x-axis.
		if(transform.position.x <= -9.2)
		{
			 transform.position = new Vector2(-9.2f, transform.position.y);
		}
		else if (transform.position.x >= 9.2f)
		{
    		transform.position = new Vector2(9.2f, transform.position.y);
    	}

    	// y-axis.
    	if (transform.position.y <= -4.5f)
    	{
    		transform.position = new Vector2(transform.position.x, -4.5f);
 		} 
 		else if (transform.position.y >= 5.0f)
 		{
     		transform.position = new Vector2(transform.position.x, 5.0f);
 		}
	
	}
}
