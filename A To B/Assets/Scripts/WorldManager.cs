using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour {

	public GameObject inGameMenu;
	public GameObject pauseMenu;
	public string[] levels;
	public static string[] statLevels;
	public static int currentLevel;
	public bool paused;
	public static bool pausedStat;

	// Use this for initialization
	void Start () {
		paused = false;
		pausedStat = false;
		statLevels = levels;
		if(SceneManager.GetActiveScene().name.Equals("Level 1"))
			{
				currentLevel = 1;
			}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
			pausedStat = !pausedStat;
			if (paused) {
				Time.timeScale = 0.0f;
				pauseMenu.gameObject.SetActive (true);
				inGameMenu.gameObject.SetActive (false);
			} 
			else 
			{
				Time.timeScale = 1.0f;
				pauseMenu.gameObject.SetActive (false);
				inGameMenu.gameObject.SetActive (true);
			}
		}
			

	}
    public void setPause(bool state)
    {
        paused = state;
        pausedStat = state;
    }
	public static void goToLevel(int pos)
	{
		SceneManager.LoadScene (statLevels [pos % statLevels.Length]);
	}

	public void goToLevel (string name)
	{
		SceneManager.LoadScene (name);
	}

	public static void nextLevel()
	{
		currentLevel++;
		SceneManager.LoadScene (statLevels[currentLevel % statLevels.Length]);
	}

	public static bool isPaused()
	{
		return pausedStat;
	}

	public static void setCurrentLevel(int lev)
	{
		currentLevel = lev % statLevels.Length;
	}

    public void setTimeScale(float f)
    {
        Time.timeScale = f;
    }
}
