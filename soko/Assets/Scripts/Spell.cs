using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

	public float lifeTime;
	public float distance;
	public float damage;
	public LayerMask solid;
	public GameObject destroyEffect;
	public float randomDamage;
	public const float minDamage = 0;
	public const float maxDamage = 100;

	// Set base multiplier to 1.
	public float damageMultiplier = 1;
	
	void Start ()
	{
		Invoke("DestroyProjectile", lifeTime);
	}
	
	void Update ()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,transform.up, distance, solid);

		if(hitInfo.collider != null)
		{
			if(hitInfo.collider.CompareTag("Enemy"))
			{
				// Randomize damage.
				randomDamage = Random.Range(minDamage,maxDamage);
				damage = randomDamage * damageMultiplier;
				
				// Make total damage clean and add up to 100.
				float enemyHealth = hitInfo.collider.GetComponent<EnemyHealth>().getHealth();
				damage = Mathf.RoundToInt(damage);
				if(damage > enemyHealth)
				{
					damage = enemyHealth;
				}
				hitInfo.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
			}
			DestroyProjectile();
		}
		
	}

	void DestroyProjectile()
	{
		Destroy(gameObject);
	}
}
