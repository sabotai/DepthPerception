using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

	bool condition1, condition2;

	// Use this for initialization
	void Start () {
		condition1 = false;
		condition2 = false;

	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit rayHit = new RaycastHit ();//blank container for info
		Ray ray = new Ray (transform.position, transform.forward);
		
		if (Physics.Raycast (ray, out rayHit, 6f)){
			Collider collider1 = rayHit.collider;
			Debug.DrawLine(ray.origin, rayHit.point);
			
			if (collider1.name == "windowPane" ){
				condition1 = true;
				Debug.Log ("looked at window");
			}
			if (Application.loadedLevel == 0){
				//if (collider1.name == "alec_alt_wearing_rift_trigger1"){
					condition2 = true;
				//}
			} else if (Application.loadedLevel == 1){
						if (collider1.name == "riftCollider" ){
							condition2 = true;
							Debug.Log ("looked at rift");

					if (PlayerPrefs.GetInt("layerProgress") > 5){
						condition1 = true;
						}
					}

					}
				
			}

		if (condition1 && condition2){
		
			if (Application.loadedLevel == 1){

				if (Physics.Raycast (transform.position, transform.forward, out rayHit, 10f)){
					Collider collider2 = rayHit.collider;
					if (collider2.name == "riftColliderLarge" || collider2.name == "riftCollider"){
						Debug.Log ("looked at one of the rift colliders");
						GameObject.Find ("reverseDir").GetComponent<TriggerSeek>().activateSeek = true;
					} else {
						GameObject.Find ("reverseDir").GetComponent<TriggerSeek>().activateSeek = false;
					}
				}
			} else if (Application.loadedLevel == 0){
					
				
				GameObject fader = GameObject.Find("guiFader");
				fader.GetComponent<ScreenFadeInOut>().fadeOut = true;
				/*
					GameObject room = GameObject.Find("1208+center");
					if (room.GetComponent<easeOutPosition>().enabled == false){
						room.GetComponent<easeOutPosition>().terminateSceneMode = true;
						room.GetComponent<easeOutPosition>().runOnce = true;
						room.GetComponent<easeOutPosition>().enabled = true;
					}
				*/
			}

		}
	
	}
}
