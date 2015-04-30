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
				
			
			}
			if (currentState > 4){
				right.GetComponent<SunShafts> ().enabled = true;
				left.GetComponent<SunShafts> ().enabled = true;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	}
}
