using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) || Input.GetAxisRaw("Grain") != 0f)
        {
            GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
        }
	}
}
