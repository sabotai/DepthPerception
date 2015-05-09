using UnityEngine;
using System.Collections;

public class ResetTimer : MonoBehaviour {

	public Transform observedObject;
	private Vector3 lastFramePos;
	private Quaternion lastFrameRot;
	private float timeSinceActivity;
	public float timeOut = 30f;

	// Use this for initialization
	void Start () {
		lastFramePos = observedObject.localPosition;
		lastFrameRot = observedObject.localRotation;
		timeSinceActivity = Time.time;

	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if ((lastFramePos != observedObject.localPosition) || (lastFrameRot != observedObject.localRotation)){
			Debug.Log ("normal activity");
			timeSinceActivity = Time.time;
			
		} 
		
		if ((lastFramePos == observedObject.localPosition) && (lastFrameRot == observedObject.localRotation)){
			Debug.Log ("time since activity:  " + (Time.time - timeSinceActivity));
			if (Time.time - timeSinceActivity > timeOut){
				reset();
				timeSinceActivity = Time.time;
			}
		}
*/
		if ((lastFrameRot != observedObject.localRotation)){
			//Debug.Log ("normal activity");
			timeSinceActivity = Time.time;
			
		} 
		
		if (lastFrameRot == observedObject.localRotation){
			//Debug.Log ("time since activity:  " + (Time.time - timeSinceActivity));
			if (Time.time - timeSinceActivity > timeOut){
				reset();
			}
		}
		
		lastFramePos = observedObject.localPosition;
		lastFrameRot = observedObject.localRotation;
	}

	void reset(){


		Debug.Log ("resetting dp due to inactivity");
		PlayerPrefs.SetInt("layerProgress", 0);


		
		//GameObject fader = GameObject.Find("guiFader");
		//fader.GetComponent<ScreenFadeInOut>().EndScene(0);
		timeSinceActivity = Time.time;
		Application.LoadLevel(0);
	}
}
