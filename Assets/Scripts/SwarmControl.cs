using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmControl : MonoBehaviour {

	public delegate void MoveAction (Vector3 destination, float radius);
	public static event MoveAction OnMoveAction;

	public delegate void AttackAction (GameObject target, float radius);
	public static event AttackAction OnAttackAction;

	public Texture2D cursorTexture;
	public GameObject minion;
	public GameObject bloodPack;

	public float moveRadius = 4f;
	public float attackRadius = 2f;

	void OnMouseEnter()
	{
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
	}

	void OnMouseExit()
	{
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000, LayerMask.GetMask("ground", "enemy", "necro"))) {
				int hitLayer = 1<<hit.collider.gameObject.layer;

				if (hitLayer == LayerMask.GetMask ("ground")) {
					MoveMinionsToPosition (hit.point);
				} else if (hitLayer == LayerMask.GetMask ("enemy")) {
					OrderMinionsToAttack (hit.collider.gameObject);
				} else if (hitLayer == LayerMask.GetMask ("necro")) {
					NecromanceTheDead (hit.collider.gameObject);
				}
			}
		}
			
		if (Input.touchCount > 0) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			if (Physics.Raycast (ray, out hit, 1000, LayerMask.GetMask("ground", "enemy", "necro"))) {
				int hitLayer = 1<<hit.collider.gameObject.layer;

				if (hitLayer == LayerMask.GetMask ("ground")) {
					MoveMinionsToPosition (hit.point);
				} else if (hitLayer == LayerMask.GetMask ("enemy")) {
					OrderMinionsToAttack (hit.collider.gameObject);
				} else if (hitLayer == LayerMask.GetMask ("necro")) {
					NecromanceTheDead (hit.collider.gameObject);
				}
			}
		}
	}

	private void NecromanceTheDead(GameObject dead) {
		Instantiate (bloodPack, dead.transform.position, Quaternion.identity);
		Instantiate (minion, dead.transform.position, Quaternion.identity);

		Destroy (dead.transform.parent.gameObject);
	}

	private void MoveMinionsToPosition(Vector3 destination) {
		if (OnMoveAction != null) {
			int listenerCount = OnMoveAction.GetInvocationList ().Length;
			if (listenerCount <= 3) {
				OnMoveAction (destination, moveRadius / 3f);
			} else if (listenerCount <= 6) {
				OnMoveAction (destination, moveRadius / 2f);
			} else {
				OnMoveAction (destination, moveRadius);
			}
		}

		MinionAudioManager.instance.ReportMinionsRunning ();
	}

	private void OrderMinionsToAttack(GameObject enemy) {
		if (OnAttackAction != null) {
			OnAttackAction (enemy, attackRadius);
		}

		MinionAudioManager.instance.ReportMinionsRunning ();
	}
}
