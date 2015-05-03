using UnityEngine;
using System.Collections;
using System.Reflection;

public class fadeIn : MonoBehaviour {

	//public string scriptTitle;

	private Component dofs, eden; 
	private FieldInfo field1, field1A;
	private FieldInfo field2, field2A;
	public float f1Pos = 3f;
	private float f1PosA;
	private float f2Pos, f2PosA;
	private bool upDownSwitch;
	public float lowThresh = 0.17f;
	public float highThresh = 3f;
	public float increment = 0.01f;
	private int currentLayer;

	// Use this for initialization
	void Start () {
		/*
		Component[] components = GameObject.Find("RightEyeAnchor").GetComponents<Component>();
		foreach (Component c in components) {
			Debug.Log(c.GetType());
		}

		*/
		currentLayer = PlayerPrefs.GetInt("layerProgress");

		dofs = GetComponent<DepthOfFieldScatter>();
		field1 = dofs.GetType().GetField("focalLength");
		field2 = dofs.GetType().GetField("aperture");
		f1Pos = 3f;
		f2Pos = 14f + (currentLayer/8f);

		f1PosA = 0f;
		f2PosA = 5f;
		
		//lowThresh = 0.17f;
		//highThresh = 3f;
		//increment = 0.01f;
		upDownSwitch = false;
		
		GameObject.Find("RightEyeAnchor").GetComponent<DepthOfFieldScatter> ().maxBlurSize += (currentLayer * 25);
		GameObject.Find("LeftEyeAnchor").GetComponent<DepthOfFieldScatter> ().maxBlurSize+= (currentLayer * 25);

		
		eden = GetComponent<EdgeDetectEffectNormals>();
		field1A = eden.GetType().GetField("edgesOnly");
		field2A = eden.GetType().GetField ("luminanceThreshold");

	}
	
	// Update is called once per frame
	void Update () {

		if ((f1Pos >= lowThresh) && (f1Pos <= highThresh)) {
			//Debug.Log ("increase pos");
			field1.SetValue (dofs, f1Pos);
			field2.SetValue (dofs, f2Pos);
			f1Pos -= increment * Time.deltaTime;
			if (f2Pos < 26.6){

				f2Pos += increment * Time.deltaTime * 50f;}
		}
		if (f1PosA < 1) {
			
			field1A.SetValue (eden, f1PosA);
			f1PosA+=0.01f* Time.deltaTime * 50f;		
		}
		if (f2PosA > 0.0001f){
			//field2A.SetValue (eden, f2PosA);
			f2PosA -= 0.01f* Time.deltaTime * 50f;
		}
	}

}
