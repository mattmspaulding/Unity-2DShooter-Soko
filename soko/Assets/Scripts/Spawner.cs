using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public enum SpawnState{SPAWNING, WAITING, COUNTING};

	public Wave[] waves;
	public int nextWave = 0;
	public int waveNumber = 1;

	public float timeBtwWaves = 1f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	public Transform[] spawnPoints;

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	private SpawnState state = SpawnState.COUNTING;

	void Start ()
	{
		waveCountdown = timeBtwWaves;
	}
	
	void Update ()
	{
		if(state == SpawnState.WAITING)
		{
			if(!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}
		if(waveCountdown <= 0)
		{
			if(state != SpawnState.SPAWNING)
			{
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted(){
		state = SpawnState.COUNTING;
		waveCountdown = timeBtwWaves;

		if(nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
		}
		else
		{
			nextWave++;
		}

		waveNumber++;
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if(GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;
		// Number of additional enemies that spawn with each new wave.
		_wave.count += 3;

		for(int i = 0; i<_wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f/_wave.rate);
		}

		state = SpawnState.WAITING;
		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Instantiate(_enemy, _sp.position, _sp.rotation);
	}
	
}

