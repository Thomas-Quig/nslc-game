using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {
	public float slowRatio;
	public Image image;
    public Vector3 spawnPosition;
    private float startTime;
    private float levelTime;
    public Canvas endCanvas;
    public Image bronze, silver, gold;
    public Vector3 medalTimes;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        bronze.gameObject.SetActive(false);
        silver.gameObject.SetActive(false);
        gold.gameObject.SetActive(false);

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
            RestartLevel();
		}
	    if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
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
            if((levelTime < medalTimes.z) && (levelTime >= medalTimes.y))
            {
                bronze.gameObject.SetActive(true);
            }
            else if ((levelTime < medalTimes.y) && (levelTime >= medalTimes.x))
            {
                silver.gameObject.SetActive(true);
            }
            else if ((levelTime < medalTimes.x))
            {
                gold.gameObject.SetActive(true);
            }
            TextManager.updateText("" + System.Math.Round(levelTime, 2));
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
