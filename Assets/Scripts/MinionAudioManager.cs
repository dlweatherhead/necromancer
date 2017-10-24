using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MinionAudioManager : MonoBehaviour {

	public static MinionAudioManager instance;

	public AudioClip minionDeath;
	public AudioClip minionRunning;
	public AudioClip minionAttacking;

	private AudioSource audioSource;

	void Awake () {
		instance = this;
	}

	void Start() {
		audioSource = GetComponent<AudioSource> ();
	}
	
	public void ReportMinionDeath() {
		audioSource.clip = minionDeath;
		audioSource.loop = false;
		audioSource.Play ();
	}

	public void ReportMinionsAttack() {
		audioSource.clip = minionAttacking;
		audioSource.Play ();
	}

	public void ReportMinionsRunning() {
		audioSource.clip = minionRunning;
		audioSource.loop = true;
		audioSource.Play ();
	}

	public void ReportMinionsStopped() {
		audioSource.Stop ();
	}
}
