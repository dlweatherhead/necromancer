using UnityEngine;

public class OutOfBoundsCleaner : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Destroyed " + other.gameObject.name);
		Destroy (other.gameObject);
	}
}
