using UnityEngine;
using System.Collections;

public class modulateSunShafts : MonoBehaviour {


	public GameObject[] cameraObject;// = new GameObject[howManyCameras];
	private float[] originalValue;// = new float[howManyCameras];
	public float[] speed;// = new GameObject[howManyCameras];
	private int currentLayer;
	public AudioSource optionalAudio;

	// Use this for initialization
	void Start () {
		currentLayer = PlayerPrefs.GetInt("layerProgress");
		originalValue = new float[cameraObject.Length];
				for (int i = 0; i < cameraObject.Length; i++) {
					originalValue [i] = cameraObject [i].GetComponent<SunShafts> ().sunShaftIntensity;
						
			
					if (Application.loadedLevel == 1){
						float amount = currentLayer/2;
						cameraObject [i].GetComponent<SunShafts> ().sunShaftBlurRadius = amount * 1.33f;

				if (currentLayer > 8){
					cameraObject[i].GetComponent<SunShafts> ().useDepthTexture = false; //stop using the z buffer to spread the color
					cameraObject[i].GetComponent<SunShafts> ().radialBlurIterations =  1; //looks weird if its too blurry
				} else {
					cameraObject[i].GetComponent<SunShafts> ().radialBlurIterations =  (int)amount-1;
				}
					}
				}


		}

	void Update () {
		float localSpeed;

		for (int i = 0; i < cameraObject.Length; i++) {
			if (cameraObject[i].GetComponent<SunShafts> ().enabled){
			if (i > speed.Length){
				localSpeed = Mathf.PingPong (Time.time * speed[speed.Length-1], originalValue[i]);
			} else {
				localSpeed = Mathf.PingPong (Time.time * speed[i], originalValue[i]);
			}
			cameraObject[i].GetComponent<SunShafts> ().sunShaftIntensity = localSpeed;
			//Debug.Log ("localSpeed = " + localSpeed + " and " + originalValue[i]);
			if (Application.loadedLevel == 1){

				if (!GameObject.Find ("guiFader").GetComponent<ScreenFadeInOut>().fadeOut){
				optionalAudio.volume = 0.1f + localSpeed/originalValue[i]; //fluctuate the volume with the sunshafts-- .1 is the minimum
				} else {
					optionalAudio.volume -= 0.1f * Time.deltaTime; //fade it out when the time comes

				}
			}
		}
		}
	}
}
