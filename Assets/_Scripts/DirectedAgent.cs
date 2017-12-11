using UnityEngine;
using UnityEngine.AI;

public class DirectedAgent : MonoBehaviour {

	private NavMeshAgent agent;
	
	void Awake () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				agent.destination = hit.point;
				agent.isStopped = false;
			}
		}
	}
}
