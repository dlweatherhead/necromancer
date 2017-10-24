using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : UnitStats, DeathEventInterface {
	public float speed = 1f;
	public float attackThreshold = 2.5f;

	public GameObject necroPrefab;

	private bool canPerformNextAttack = true;

	private Vector3 guardStation;

	private UnitStats target;

	private Animator animator;

	private Vector3 attackOffset = Vector3.zero;

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (target != null) {
			bool inAttackRange = Vector3.Distance (transform.position, target.transform.position) <= attackThreshold;
			if (inAttackRange) {
				animator.SetBool ("isRunning", false);
				animator.SetBool ("isAttacking", true);

				Vector3 lookPoint = target.transform.position;
				lookPoint.y = 0.5f;
				transform.LookAt (lookPoint);

				if (canPerformNextAttack) {
					StartCoroutine (Attack ());
				}
			} else {
				animator.SetBool ("isRunning", true);
				animator.SetBool ("isAttacking", false);
				MoveToLocation (target.transform.position + attackOffset);
			}
		} else {
			if (transform.localPosition.Equals (new Vector3(0f, 0.5f, 0f))) {
				animator.SetBool ("isRunning", false);
				animator.SetBool ("isAttacking", false);
			} else {
				MoveToLocation (guardStation);
			}
		}
	}

	private void MoveToLocation(Vector3 location) {
		Vector3 target = location;
		target.y = 0.5f;
		animator.SetBool ("isRunning", true);
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target, step);
		transform.LookAt (target);
	}

	public void setGuardStation(GameObject guardStation) {
		this.guardStation = guardStation.transform.position;
	}

	public void NotifyPerimeterBreached(UnitStats target) {
		if (this.target == null) {
			this.target = target;
		}
	}

	private void CalculateAttackOffset() {
		float angle = Random.Range (0f, 360f);
		float x = attackThreshold * Mathf.Cos (angle);
		float z = attackThreshold * Mathf.Sin (angle);
		attackOffset = new Vector3 (x, 0f, z);

		Debug.Log ("Attack offset");
	}

	public void OnDeathEvent() {
		if(!animator.GetBool("isDead")) {
			StartCoroutine (Die ());
		}
	}

	private IEnumerator Attack() {
		canPerformNextAttack = false;
		target.OnReceiveDamage(damage);

		yield return new WaitForSeconds (1f);

		if (animator.GetBool ("isDead")) {
			canPerformNextAttack = false;
		} else {
			canPerformNextAttack = true;
		}
	}

	private IEnumerator Die() {
		animator.SetBool ("isRunning", false);
		animator.SetBool ("isAttacking", false);
		animator.SetBool ("isDead", true);
		canPerformNextAttack = false;
		yield return new WaitForSeconds (1f);
		Instantiate (necroPrefab, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
