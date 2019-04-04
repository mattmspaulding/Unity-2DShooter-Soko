using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public float enemyMaxHealth = 100;
	public float enemyHealth;

	public int scoreValue = 10;
	public static int totalScore = 0;
	public SpriteRenderer enemyBody;
	public Color hurtColor;

	public GameObject CBTprefab;

	//public GameObject deathSplatter;

	void Start ()
	{
		enemyHealth = enemyMaxHealth;
	}
	
	void Update ()
	{
		// If the enemy dies, add to score, spawn items, and destroy the enemy.
		if(enemyHealth <= 0)
		{
			totalScore += scoreValue;
			UpdateScore();
			this.GetComponent<LootManager>().SpawnItems();
			Destroy(gameObject);
		}
	}

	public void UpdateScore()
	{
		ScoreManager.score += scoreValue;
	}

	// Make the enemy's body flash when it's hit.
	IEnumerator Flash()
	{
		enemyBody.color = hurtColor;
		yield return new WaitForSeconds(0.05f);
		enemyBody.color = Color.white;
	}

	// When the enemy takes damage make it flash, take away health, then show combat text.
	public void TakeDamage(float damage)
	{
		StartCoroutine(Flash());
		enemyHealth -= damage;
		InitCBT(damage.ToString());
	}

	public float getHealth()
	{
		return enemyHealth;
	}

	// For displaying combat text.
	void InitCBT(string text)
	{
		GameObject temp = Instantiate(CBTprefab) as GameObject;
		RectTransform tempRect = temp.GetComponent<RectTransform>();
		temp.transform.SetParent(transform.Find("EnemyCanvas"));
		tempRect.transform.localPosition = CBTprefab.transform.localPosition;
		tempRect.transform.localScale = CBTprefab.transform.localScale;
		//tempRect.transform.localRotation = CBTprefab.transform.localRotation;
	
		temp.GetComponent<Text>().text = text;
		temp.GetComponent<Animator>().SetTrigger("Hit");
		Destroy(temp.gameObject, 2);
	}
}
