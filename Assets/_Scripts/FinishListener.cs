using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishListener : MonoBehaviour {

	public GameObject wintext;
	public GameObject loseText;

	void Start() {
		wintext.SetActive(false);
		loseText.SetActive (false);
	}

	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");	
		if (enemies.Length == 0) {
			wintext.SetActive(true);
		}

		GameObject[] minions = GameObject.FindGameObjectsWithTag ("Minion");
		if (minions.Length == 0) {
			loseText.SetActive (true);
		} else {
			loseText.SetActive (false);
		}
	}
}
