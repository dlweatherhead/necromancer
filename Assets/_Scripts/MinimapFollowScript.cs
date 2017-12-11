using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollowScript : MonoBehaviour {

	public Transform target;
	
	void Update () {
		var newPos = new Vector3(target.position.x, transform.position.y, target.position.z);
		transform.position = newPos;
	}
}
