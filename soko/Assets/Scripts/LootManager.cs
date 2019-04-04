using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot{
	public GameObject lootObject;
	public float spawnChance;
}

public class LootManager : MonoBehaviour {

	public List<Loot> loot = new List<Loot>();
	public GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void SpawnItems()
	{
		for(int i = 0; i < loot.Count; i++)
		{
			if(Random.value * 100 < loot[i].spawnChance)
			{
				Debug.Log(loot[i].lootObject);
				// Check if player needs health before potentially spawning a heart.
				if(!(loot[i].lootObject.name == "heart"))
				{
					Instantiate(loot[i].lootObject, transform.position, Quaternion.identity);
				}
				else if(player.GetComponent<PlayerHealth>().HealthCheck())
				{
					Instantiate(loot[i].lootObject, transform.position, Quaternion.identity);
				}
				
			}
		}
	}
}

