using UnityEngine;
using System.Collections;

public class TriggerSeek : MonoBehaviour {

	public Transform comeToMe;
	public Transform lookAtMe;
	public bool activateSeek;
	public float transitionThreshold = 0.9999f;
	public float freezeThresh = 0.98f;

	private Vector3 totalDistance;
	private GameObject faderObj;

	private GameObject OVRPlayer;
	public GameObject newParent;

	// Use this for initialization
	void Start () {
		totalDistance = comeToMe.position - transform.position;
		faderObj = GameObject.Find ("guiFader");

		OVRPlayer = GameObject.Find ("OVRPlayerController");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		
		//if (Input.GetKey(KeyCode.Space)) activateSeek = true;
		Vector3 temp = comeToMe.position - transform.position;
		float tempMag =  1f - temp.sqrMagnitude / totalDistance.sqrMagnitude;
		//Debug.Log ("tempMag = " + tempMag);
		if (tempMag > .995f) activateSeek = true;

		if (Input.GetKey(KeyCode.Space) || activateSeek){
			//find the percentage to destination
			//Debug.Log ("difference is " + tempMag);
			
			transform.LookAt (lookAtMe);

			if (tempMag < transitionThreshold){
				transform.position = Vector3.Lerp (transform.position, comeToMe.position, Time.deltaTime);
				if (tempMag > freezeThresh){
					//deparent.transform.SetParent(newParent.transform, true);
					//deparent.transform.parent = null;
					OVRPlayer.GetComponent <OVRPlayerController>().enabled = false;
					OVRPlayer.GetComponent <OVRGamepadController>().enabled = false;
					OVRPlayer.GetComponent <MouseLook>().enabled = false;
				} else {

				}
			} else {
				//find the fadeout bool in the screenfadeinout script
				faderObj.GetComponent <ScreenFadeInOut>().fadeOut = true;

			}

		}
	}


}
