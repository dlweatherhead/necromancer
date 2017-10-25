using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject corpse;

	public int numberOfCorpses;

	void Update() {
		MapGenerator mapGen = GetComponent<MapGenerator>();

		if (Input.GetMouseButtonDown(0)) {
			mapGen.GenerateMap();

//			Position[] validPositions = GenerateMapOfValidPositions (map);
//
//			PopulateMapWithObject (validPositions, numberOfCorpses, corpse);
		}
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

			Instantiate (obj, new Vector3 (map [randomPosition].x, -1, map [randomPosition].y), Quaternion.identity);
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
