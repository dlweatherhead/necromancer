using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GuardStation : MonoBehaviour {

	public Enemy enemyChild;

	void Start() {
		enemyChild = GetComponentInChildren<Enemy> ();
		enemyChild.setGuardStation (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		enemyChild.NotifyPerimeterBreached (other.GetComponent<UnitStats>());
	}
}
