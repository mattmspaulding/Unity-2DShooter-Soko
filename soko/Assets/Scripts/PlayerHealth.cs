using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	public int health;
	public int numOfHearts;

	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;

	public bool isDead = false;
	//public GameObject deathEffect;

	AudioSource deathSound;

	void Start()
	{
		deathSound = GetComponent<AudioSource>();
	}

	void Update()
	{

		if(health > numOfHearts)
		{
			health = numOfHearts;
		}

		for(int i = 0; i < hearts.Length;i++)
		{

			if(i < health)
			{
				hearts[i].sprite = fullHeart;
			}
			else
			{
				hearts[i].sprite = emptyHeart;
			}

			if(i < numOfHearts)
			{
				hearts[i].enabled = true;
			}
			else
			{
				hearts[i].enabled = false;
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Enemy")
		{
			health -= 1;
			Destroy(coll.gameObject);
			if(health <= 0)
			{
				isDead = true;
				deathSound.Play();
				StartCoroutine(DeathEffect());
			}
		}
	}

	IEnumerator DeathEffect()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity);

		// Play audio for death.
		deathSound.Play();

		yield return new WaitForSeconds(1);

		// Change scene to game over.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	// Returns true if player needs health
	public bool HealthCheck()
	{
		if(health < hearts.Length)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void AddHeart()
	{
		health += 1;
	}
}
