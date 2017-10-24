using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour {

	public void OnRetryPressed() {
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}

	public void OnQuitPressed() {
		Application.Quit ();
	}
}
