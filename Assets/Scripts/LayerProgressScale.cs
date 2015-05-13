using UnityEngine;
using System.Collections;

public class LayerProgressScale : MonoBehaviour {

	private int currentState;
	public Renderer rend;
	public Material mat;
	public float alphaMult = 5;
	private GameObject right, left;

	// Use this for initialization
	void Start () {
		currentState = PlayerPrefs.GetInt("layerProgress");
		Debug.Log ("currentState is " + currentState);
		//rend = GetComponent<Renderer> ();
		right = GameObject.Find("RightEyeAnchor");
		left  = GameObject.Find("LeftEyeAnchor");

		if (Application.loadedLevel == 0){
			mat.SetColor ("_Color", new Color(1.0f, 1.0f, 1.0f, (currentState * alphaMult)/100f));

		} else {
			//if it is room 1208 and the state is past the threshold, run the effect 
			if (currentState > 2){
				(right.GetComponent("ScreenOverlay") as MonoBehaviour).enabled = !(GameObject.Find("RightEyeAnchor").GetComponent("ScreenOverlay") as MonoBehaviour).enabled;
				(left.GetComponent("ScreenOverlay") as MonoBehaviour).enabled = !(GameObject.Find("LeftEyeAnchor").GetComponent("ScreenOverlay") as MonoBehaviour).enabled;
				//Debug.Log ("screenoverlay status = " + (GameObject.Find("RightEyeAnchor").GetComponent("ScreenOverlay") as MonoBehaviour).enabled);
				left.GetComponent<ScreenOverlay>().intensity = currentState;
				right.GetComponent<ScreenOverlay>().intensity = currentState;
			}
			if (currentState > 4){
				right.GetComponent<SunShafts> ().enabled = true;
				left.GetComponent<SunShafts> ().enabled = true;
			}
			if (currentState > 7){
				if (currentState < 10){
				left.GetComponent<EdgeDetectEffectNormals>().edgesOnly += (currentState-7) * 0.1f;
				right.GetComponent<EdgeDetectEffectNormals>().edgesOnly += (currentState-7) * 0.1f;
				} else {
					
					left.GetComponent<EdgeDetectEffectNormals>().edgesOnly += 0.25f + (currentState-7) * 0.01f;
					right.GetComponent<EdgeDetectEffectNormals>().edgesOnly += 0.25f +(currentState-7) * 0.01f;
				}
			}

			if (currentState > 8){
				right.GetComponent<DepthOfFieldScatter> ().enabled = true;
				left.GetComponent<DepthOfFieldScatter> ().enabled = true;
				
				right.GetComponent<DepthOfFieldScatter> ().focalLength = 8 * (currentState-10)/2;
				left.GetComponent<DepthOfFieldScatter> ().focalLength = 8 * (currentState-10)/2;
				
				left.GetComponent<DepthOfFieldScatter> ().maxBlurSize = 30;
				right.GetComponent<DepthOfFieldScatter> ().maxBlurSize = 30;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	}
}
