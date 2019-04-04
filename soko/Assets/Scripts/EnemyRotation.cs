using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotates enemy to face player.
public class EnemyRotation : MonoBehaviour {

	public Transform target;
	private Vector3 difference;

	// Angle between x-axis and a 2D vector.
	private float angle;

	public float offset = 80;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	void Update ()
	{
		difference = (target.position - transform.position);
		angle = Mathf.Atan2(difference.y, difference.x);
		transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg + offset);

		//difference = (target.position - transform.position);
		//angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		// transform.Rotate(0f,0f,angle)


	}
}
