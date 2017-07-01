﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {
	public float slowRatio;
	public Image image;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!WorldManager.isPaused()) {
			if (Input.GetKey(KeyCode.Mouse0)) {
				Time.timeScale = slowRatio;
				Debug.Log (Time.timeScale);
				image.gameObject.SetActive (true);

			}
			else if (Input.GetKeyUp (KeyCode.Mouse0)) {
				Time.timeScale = 1.0f;
				image.gameObject.SetActive (false);
			}
		}
		if (transform.position.y < -30) {
			transform.position = new Vector3 (0, 5, 0);
		}
			
	}

	public void OnTriggerEnter(Collider col)
	{
		Debug.Log ("ENTER");
		if(col.gameObject.tag.Equals("FinishPad"))
		{
			WorldManager.nextLevel ();
		}
			
	}
}
