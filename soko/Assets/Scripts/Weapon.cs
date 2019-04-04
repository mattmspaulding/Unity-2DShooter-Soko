using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the weapon and spells.
public class Weapon : MonoBehaviour {

	public Joystick weaponJoystick;
	public float offset;
	public GameObject spell;
	public GameObject spellImage;
	public Transform shotPoint;
	public float spellVel;
	public float timeBtwShots;
	public float startTimeBtwShots;

	void Update()
    {
    	// Weapon rotation.
        Vector3 lookVec = new Vector3(weaponJoystick.Horizontal, 
            weaponJoystick.Vertical, 4000);
        Quaternion rotationAmount = Quaternion.Euler(0, 0, offset);
        transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back) * rotationAmount;

        if(timeBtwShots <= 0)
        {
			if(weaponJoystick.Horizontal != 0 && weaponJoystick.Vertical !=0)
			{

				// Spell rotation.
            	Vector2 direction = (Vector2)((lookVec));
           		direction.Normalize();

				// Create the spell.
            	GameObject spell = (GameObject)Instantiate(spellImage, shotPoint.position, transform.rotation);

            	// Add velocity to the bullet
            	spell.GetComponent<Rigidbody2D>().velocity = direction * spellVel;
				
				timeBtwShots = startTimeBtwShots;
			}
		}
		else
		{
			timeBtwShots -= Time.deltaTime;
		}
    }
}
