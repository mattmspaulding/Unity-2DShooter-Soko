using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys heart if player doesn't need one.
public class Heart : MonoBehaviour{
	public GameObject player;

	void Start ()
	{
		player = GameObject.FindWithTag("Player");
	}
	
	void Update ()
	{
		if(player.GetComponent<PlayerHealth>().HealthCheck() == false)
		{
			Destroy(gameObject);
		}
	}
}
