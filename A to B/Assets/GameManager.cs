using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour {

	public GameObject inGameMenu;
	public GameObject pauseMenu;
	public static bool paused;
	// Use this for initialization
	void Start () {
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
		}

		if (paused) {
			Time.timeScale = 0.0f;
			pauseMenu.gameObject.SetActive (true);
			inGameMenu.gameObject.SetActive (false);
		} else {
			Time.timeScale = 1.0f;
			pauseMenu.gameObject.SetActive (false);
			inGameMenu.gameObject.SetActive (true);
			Cursor.lockState = CursorLockMode.Locked; 
		}

	}

	public static bool isPaused()
	{
		return paused;
	}
}
