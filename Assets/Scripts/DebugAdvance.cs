using UnityEngine;
using System.Collections;

public class DebugAdvance : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Backspace)){
			GameObject fader = GameObject.Find("guiFader");
			fader.GetComponent<ScreenFadeInOut>().fadeOut = true;
		}

	}
}
