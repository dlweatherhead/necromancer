using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmFollow : MonoBehaviour {

	[Range(0f,0.6f)]
	public float swarmInMotionLag = 0f;
	[Range(0.6f, 1f)]
	public float swarmHaltedLag = 0.9f;

	private List<Minion> swarm;
		
	void Start() {
		GameObject o = GameObject.FindGameObjectWithTag ("Starting Point");
		if (o == null) {
			o = GameObject.Find ("Starting Point");
		}
		transform.position = o.transform.position;
	}

	void Update() {

		FindSwarm ();

		float step = CalculateAverageSwarmSpeed () * Time.deltaTime;
		if (IsSwarmInMotion ()) {
			step *= swarmInMotionLag;
		} else {
			step *= swarmHaltedLag;
		}

		Vector3 center = CalculateSwarmCentre ();
		transform.position = Vector3.MoveTowards(transform.position, center, step);
	}

	private void FindSwarm() {
		swarm = new List<Minion> ();

		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Minion");
		foreach (GameObject m in objects) {
			swarm.Add (m.GetComponent<Minion> ());
		}
	}

	private bool IsSwarmInMotion() {
		for (int i = 0; i < swarm.Count; i++) {
			if (swarm [i].isMoving) {
				return true;
			}
		}
		return false;
	}

	private Vector3 CalculateSwarmCentre() {
		int swarmSize = swarm.Count;
		if (swarmSize == 0) {
			return transform.position;
		} 
		Vector3 average = Vector3.zero;
		for (int i = 0; i < swarmSize; i++) {
			average += swarm [i].transform.position;
		}
		return average / swarmSize;
	}

	private float CalculateAverageSwarmSpeed() {
		float swarmSize = swarm.Count;
		float averageFollowSpeed = 0f;
		for (int i = 0; i < swarmSize; i++) {
			averageFollowSpeed += swarm [i].speed;
		}
		return averageFollowSpeed / swarmSize;
	}
}
