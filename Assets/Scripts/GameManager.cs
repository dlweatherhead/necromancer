using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

//	private TextMeshProUGUI textMesh;

	void Start() {
//		textMesh = GameObject.FindGameObjectWithTag ("Minion Count").GetComponent<TextMeshProUGUI>();
	}

	void Update() {
		int minions = GameObject.FindGameObjectsWithTag ("Minion").Length;
//		textMesh.text = "<color=white>Minions: </color><color=red><b>" + minions + "</b></color>";
	}
}
