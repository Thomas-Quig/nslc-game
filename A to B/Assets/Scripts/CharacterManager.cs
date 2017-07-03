using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {
	public float slowRatio;
	public Image image;
    public Vector3 spawnPosition;
	// Use this for initialization
	void Start () {
		
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
		if (transform.position.y < -30) {
			transform.position = spawnPosition;
		}
			
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag.Equals("FinishPad"))
		{
			WorldManager.nextLevel ();
		}
			
	}

    public void RestartLevel()
    {
        transform.position = spawnPosition;
    }
}
