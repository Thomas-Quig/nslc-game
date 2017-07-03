using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
    // Use this for initialization
    Text text;
    static string currTxt;
    
	void Start () {
       text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (text.text != currTxt)
        {
            text.text = currTxt + " seconds!";
        }
	}

    public static void updateText(string newText)
    {
        currTxt = newText;
    }
}
