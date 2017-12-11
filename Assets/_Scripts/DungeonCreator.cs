using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour {

	public float levelwidth = 50;
	public float levelHeight = 50;

	public GameObject pillar;
	public GameObject flame;
	public GameObject corpse;
	public GameObject enemy;
	public GameObject startingPoint;

	public int numberOfPillars = 10;
	public int numberOfFlames = 5;
	public int numberOfCorpses = 5;
	public int numberOfStartingCorpses = 2;
	public int[] enemyBundles;
	
	void Start() {
		SpawnStartingCorpses ();
		SpawnPillars (numberOfPillars);
		SpawnItems (numberOfFlames, flame);
		SpawnItems (numberOfCorpses, corpse);
		SpawnEnemies ();
	}

	private void SpawnStartingCorpses() {
		GameObject s = Instantiate (startingPoint);

		for (int i = 0; i < numberOfStartingCorpses; i++) {
			Instantiate (corpse, 
				s.transform.position + new Vector3 (Random.Range (-i, i),0.5f,Random.Range (-i, i)), 
				Quaternion.identity);
		}
	}

	private void SpawnPillars(int numberOfPillars) {
		int spacingWidth = (int)(levelwidth / numberOfPillars);
		int spacingHeight = (int)(levelHeight / numberOfPillars);
		for (int i = (int)(spacingWidth/2); i < levelwidth; i += spacingWidth) {
			for (int j = (int)(spacingHeight/2); j < levelHeight; j += spacingHeight) {
				Instantiate (pillar, 
					new Vector3(i + Random.Range (-2, 2), 0, j + Random.Range (-2, 2)), 
					Quaternion.identity);
			}
		}
	}

	private void SpawnItems (int number, GameObject item) {
		GameObject[] items = new GameObject [number];
		for(int i = 0; i < number; i++) {
			Vector3 newPos = GenerateValidItemLocation (items);
			items[i] = Instantiate (item, newPos, Quaternion.identity);
		}
	}

	private Vector3 GenerateValidItemLocation(GameObject[] items) {
		Vector3 result = new Vector3(Random.Range (0f, levelwidth),0.5f,Random.Range (0f, levelHeight));
		if (IsNearOtherItems (items, result)) {
			result = GenerateValidItemLocation (items);
		}
		return result;
	}

	private bool IsNearOtherItems(GameObject[] items, Vector3 pos) {
		foreach (GameObject i in items) {
			if (i == null) continue;

			float itemX = i.transform.position.x;
			float itemZ = i.transform.position.z;
			if ((pos.x > itemX - 5 && pos.x < itemX + 5) &&
				(pos.z > itemZ - 5 && pos.z < itemZ + 5)) {
				return true;
			}
		}
		return false;
	}

	private void SpawnEnemies() {
		GameObject[] enemies = new GameObject[enemyBundles.Length];
		for (int i = 0; i < enemyBundles.Length; i++) {
			Vector3 spawnPos = GenerateValidItemLocation(enemies);
			for (int j = 0; j < enemyBundles [i]; j++) {
				enemies[i] = Instantiate (enemy, 
					new Vector3 (spawnPos.x + Random.Range (-j, j),0.5f,spawnPos.z + Random.Range (-j, j)), 
					Quaternion.identity);			
			}
		}
	}
}
