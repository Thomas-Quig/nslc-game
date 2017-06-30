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
		if (!GameManager.isPaused ()) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				image.gameObject.SetActive (true);
				Time.timeScale = slowRatio;
			} else if (Input.GetKeyUp (KeyCode.Mouse0)) {
				Time.timeScale = 1.0f;
				image.gameObject.SetActive (false);
			}
		}
	}
}
