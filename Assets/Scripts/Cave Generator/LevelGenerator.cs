using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GenerateNavMesh))]
public class LevelGenerator : MonoBehaviour {

	public GameObject corpse;
	public int numberOfCorpses;
	
	public GameObject enemy;
	public int numberOfEnemies;

	private void Awake() {
		MapGenerator mapGen = GetComponent<MapGenerator>();

		mapGen.GenerateMap();

		Position[] validPositions = GenerateMapOfValidPositions (mapGen.map);
		PopulateMapWithObject (validPositions, numberOfCorpses, corpse);
		PopulateMapWithObject(validPositions, numberOfEnemies, enemy);

		var randomPosition = Random.Range(0, validPositions.Length);
		var startingPoint = new Vector3(validPositions[randomPosition].x -25, 0f, validPositions[randomPosition].y -25);
		GameObject startingCorpse = Instantiate(corpse, startingPoint, Quaternion.identity);
		startingCorpse.name = "Starting Point";
		startingCorpse.tag = "Starting Point";

		GetComponent<GenerateNavMesh>().Generate();
	}

	private Position[] GenerateMapOfValidPositions(int[,] map) {
		List<Position> positions = new List<Position> ();
		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				if (map [x, y] == 0) {
					positions.Add (new Position (x, y));
				}
			}
		}
		return positions.ToArray();
	}

	private void PopulateMapWithObject(Position[] map, int amount, GameObject obj) {
		int length = map.Length;

		for (int i = 0; i < amount; i++) {
			int randomPosition = Random.Range (0, length);

			Instantiate (obj, new Vector3 (map [randomPosition].x -25, -1.25f, map [randomPosition].y -25), Quaternion.identity);
		}
	}

	private class Position {
		public int x;
		public int y;

		public Position (int x, int y) {
			this.x = x;
			this.y = y;
		}
	}
}
