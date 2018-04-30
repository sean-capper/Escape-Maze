using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {

	[SerializeField] GameObject victoryScreen;
	CanvasGroup group;
	bool escaped = false;

	private void Start() {
		group =	victoryScreen.GetComponent<CanvasGroup>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update() {
		if(escaped) {
			FadeInUI();
			if(Input.GetKey(KeyCode.Space)) {
				SceneManager.LoadScene(0);
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player") && !escaped) {
			escaped = true;
		}
	}

	void FadeInUI() {
		group.alpha += Time.deltaTime * 0.5f;
	}

}
