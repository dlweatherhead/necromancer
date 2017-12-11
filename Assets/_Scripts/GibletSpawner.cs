using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GibletSpawner : MonoBehaviour {

	[Range(0.1f, 0.3f)]
	public float minSize = 0.1f;
	[Range(0.1f, 0.3f)]
	public float maxSize = 0.3f;

	void Start () {
		float scaleSize = Random.Range (minSize, maxSize);
		transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
	}
}
