using UnityEngine;
using UnityEngine.AI;

public class GenerateNavMesh : MonoBehaviour {

	public NavMeshSurface surface;
	
	public void Generate () {
		if (surface != null) {
			surface.BuildNavMesh();
		}
	}
}
