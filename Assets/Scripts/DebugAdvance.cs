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
		if (Input.GetKeyDown(KeyCode.Return)){
			
			GameObject room = GameObject.Find("1208+center");
			room.GetComponent<easeOutPosition>().terminateSceneMode = true;
			room.GetComponent<easeOutPosition>().runOnce = true;
			room.GetComponent<easeOutPosition>().enabled = true;

		}

	}
}
