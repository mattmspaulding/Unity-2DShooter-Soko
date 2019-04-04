using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPotion : MonoBehaviour {

	ParticleSystem potionParticles;
	float stopParticles = 5f;
	float timeOnScreen = 20f;

	void Start ()
	{
		potionParticles = GetComponentInChildren<ParticleSystem>();
	}
	
	void Update ()
	{
		stopParticles -= Time.deltaTime;
		timeOnScreen -= Time.deltaTime;

		// Stops potion particles after certain amount of time.
		if(potionParticles != null && stopParticles <= 0)
		{
			potionParticles.Stop();
		} 
		else if(timeOnScreen <= 0)
		{
			Destroy(gameObject);
		}

		// Check if potion is out of bounds
		if(transform.position.x <= -9.2)
		{
			 Destroy(gameObject);
		}
		else if (transform.position.x >= 9.2f)
		{
    		Destroy(gameObject);
    	}

    	if (transform.position.y <= -4.5f)
    	{
    		Destroy(gameObject);
 		} 
 		else if (transform.position.y >= 5.0f)
 		{
     		Destroy(gameObject);
 		}
	}	
}
