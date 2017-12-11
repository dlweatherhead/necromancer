using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour {
	
	public int health;
	public int damage;

	public void OnReceiveDamage(int damage) {

		Debug.Log ("Damage Received: " + damage);

		this.health -= damage;
		if (health < 0) {
			HandleDeathEvent ();
		}
	}

	private void HandleDeathEvent() {
		DeathEventInterface child = GetComponentInChildren<DeathEventInterface> ();
		if (child != null) {
			child.OnDeathEvent ();
		}
	}
}
