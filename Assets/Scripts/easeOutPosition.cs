using UnityEngine;
using System.Collections;

public class easeOutPosition : MonoBehaviour {

	public Transform target;
	public float tripTime = 5f;
	public float easeAmt = 0.5f; //going over 1f will make it go back to where it began
	private float currentLerpTime;
	private Vector3 startPos;
	private bool done = false;
	public bool runOnce = false;
	public bool terminateSceneMode = false;

	// Use this for initialization
	void Start () {
		
		startPos = transform.position;
		currentLerpTime = Time.time;

		
	}

	void onEnable(){

		currentLerpTime = Time.time;
		Debug.Log ("updated start time");
	}
	
	// Update is called once per frame
	void Update () {
		if (runOnce){
			//set the start time to the current time if desired to start after scene start
			currentLerpTime = Time.time;
			tripTime += currentLerpTime; //push the triptime down to account for the late start
			runOnce = false;
			Debug.Log ("easeOutPosition runOnce just ran... currentLerpTime = " + currentLerpTime);
		}


			if (!done){
				currentLerpTime += Time.deltaTime;
				if (currentLerpTime > tripTime) {
					//no longer than the trip time
					currentLerpTime = tripTime;
				}

				float t = currentLerpTime / tripTime;
				t = Mathf.Sin(t * Mathf.PI * easeAmt);

				
				//float perc = currentLerpTime / tripTime;
				transform.position = Vector3.Lerp(startPos, target.position, t);
				//Debug.Log ("t = " + t);

				if (terminateSceneMode){
					if (t >= 0.999f){
						GameObject fader = GameObject.Find("guiFader");
						fader.GetComponent<ScreenFadeInOut>().fadeOut = true;
						Debug.Log ("fadeOut activated");
						terminateSceneMode = false;
					}
				}

				if (t == 1f){
					done = true;
					Debug.Log ("ease out done");
				}
			}

	}
}
