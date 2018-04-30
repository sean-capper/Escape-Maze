using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
