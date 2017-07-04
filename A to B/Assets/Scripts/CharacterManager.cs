using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {
	public float slowRatio;
	public Image image;
    public Vector3 spawnPosition;
    public float startTime;
    public float levelTime;
    public Canvas endCanvas;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!WorldManager.isPaused()) {
			if (Input.GetKey(KeyCode.Mouse0) || Input.GetAxis("Slow Time") != 0f)
            {
				Time.timeScale = slowRatio;
				image.gameObject.SetActive (true);

			}
			else if (Input.GetKeyUp (KeyCode.Mouse0) || Input.GetAxis("Slow Time") == 0f) {
				Time.timeScale = 1.0f;
				image.gameObject.SetActive (false);
			}
		}
		if (transform.position.y < -30 || GetComponent<Rigidbody>().velocity.y < -25){
			transform.position = spawnPosition;
		}
			
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag.Equals("FinishPad"))
		{
            levelTime = Time.time - startTime;
            endCanvas.gameObject.SetActive(true);
            transform.position = new Vector3(0f, 200f, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            Cursor.lockState = CursorLockMode.None;
            Debug.Log(levelTime);
            TextManager.updateText("" + levelTime);
            WorldManager.setPauseStat(true);
            Time.timeScale = 0f;
		}
			
	}

    public void RestartLevel()
    {
        startTime = Time.time;
        transform.position = spawnPosition;
        transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
