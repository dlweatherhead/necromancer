using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Minion : UnitStats, DeathEventInterface {

	public float speed = 1f;
	public float moveDelay = 0.1f;
	public bool isMoving;

	private Vector3 newDestination;
	private float slightDelayTimerEnd = 0f;

	private GameObject attackTarget;
	private bool isAttacking;
	private Vector3 attackPositionOffset;
	private float currentAttackRadius;
	private bool canPerformNextAttack = true;

	private Animator animator;

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	void OnEnable () {
		newDestination = transform.position;
		SwarmControl.OnMoveAction += MoveToDestination;
		SwarmControl.OnAttackAction += AttackTarget;
	}

	void OnDisable () {
		SwarmControl.OnMoveAction -= MoveToDestination;
		SwarmControl.OnAttackAction -= AttackTarget;
	}

	void Update() {

		if (!isMoving) {
			MinionAudioManager.instance.ReportMinionsStopped ();
		}

		if (transform.position.Equals (newDestination)) {
			isMoving = false;
			animator.SetBool ("isRunning", false);
		}

		if (attackTarget == null) {
			animator.SetBool ("isAttacking", false);
		}

		if (isMoving) {
			if (Time.time > slightDelayTimerEnd) {
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, newDestination, step);
			}
			transform.LookAt (newDestination);
		}

		if (isAttacking && attackTarget != null) {
			if (Time.time > slightDelayTimerEnd) {
				float step = speed * Time.deltaTime;
				Vector3 attackPosition = attackTarget.transform.position + attackPositionOffset;
				attackPosition.y = 0.5f;
				transform.position = Vector3.MoveTowards (transform.position, attackPosition, step);
			}

			float targetDistance = Vector3.Distance(transform.position, attackTarget.transform.position);
			if (targetDistance <= currentAttackRadius + 0.5f) {
				if (canPerformNextAttack) {
					StartCoroutine (Attack ());
				}
			}

			Vector3 lookPosition = attackTarget.transform.position;
			lookPosition.y = 0.5f;
			transform.LookAt (lookPosition);
		}
	}

	void MoveToDestination (Vector3 destination, float radius) {
		animator.SetBool ("isRunning", true);
		isMoving = true;
		isAttacking = true;
		attackTarget = null;
		slightDelayTimerEnd = Time.time + moveDelay;
		float x = Random.Range (-radius, radius);
		float z = Random.Range (-radius, radius);
		newDestination = new Vector3 (destination.x + x, transform.position.y, destination.z + z);
	}

	void AttackTarget (GameObject target, float radius) {
		animator.SetBool ("isRunning", true);
		isMoving = false;
		isAttacking = true;
		attackTarget = target;
		currentAttackRadius = radius;

		float angle = Random.Range (0f, 360f);

		float x = radius * Mathf.Cos (angle);
		float z = radius * Mathf.Sin (angle);
		attackPositionOffset = new Vector3 (x, 0.5f, z);
	}

	public void OnDeathEvent() {

		MinionAudioManager.instance.ReportMinionDeath ();

		Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody> ();

		foreach (Rigidbody rbody in rigidbodies) {
			rbody.useGravity = true;
			rbody.isKinematic = false;
			rbody.gameObject.transform.SetParent (null);
			rbody.AddForce (new Vector3 (Random.Range (10f, 100f), Random.Range (10f, 100f), Random.Range (10f, 100f)));
		}
			
		// TODO - Inform Game Manager

		// TODO - Inform Minion AudioManager to play death sound

		Destroy (gameObject);
	}

	private IEnumerator Attack() {
		canPerformNextAttack = false;

		animator.SetBool ("isRunning", false);
		animator.SetBool ("isAttacking", true);

		attackTarget.GetComponent<UnitStats> ().OnReceiveDamage (damage);

		// TODO - Attack Effect

		yield return new WaitForSeconds (1f);
		canPerformNextAttack = true;
	}
}
