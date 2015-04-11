using UnityEngine;
using System.Collections;

public class TrackProgress : MonoBehaviour {

	public bool resetPrefs;
	// Use this for initialization
	void Start () {
		resetPrefs = false;
		/*
		if (Time.realtimeSinceStartup > 10) {
		//check if its just started running

			Debug.Log ("start: resetting playerprefs");
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetInt("layerProgress", 0);
		}
		*/

	}
	
	// Update is called once per frame
	void Update () {

		if (resetPrefs){
			
			Debug.Log ("resetting playerprefs");
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetInt("layerProgress", 0);
			resetPrefs = false;
		}
	
	}
}
