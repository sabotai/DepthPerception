using UnityEngine;
using System.Collections;

public class RandomTextureOffset : MonoBehaviour {

	//public Renderer rend;
	public Material mat;
	public float offsetScale = 5f;
	public float alphaMultiplier = 10f;
	private int currentLayer;

	// Use this for initialization
	void Start () {
		//rend = GetComponent<Renderer> ();
		currentLayer = PlayerPrefs.GetInt("layerProgress");

		//set the static transparency to inverse scene progress
		if (currentLayer < 10) {
			mat.SetColor ("_Color", new Color(1.0f, 1.0f, 1.0f, (currentLayer * alphaMultiplier)/100f));
		} else {
			//start dialing it down after 10 layers (actually 5, but whatevs)
			float curr = 1.0f-(currentLayer * alphaMultiplier)/50f;
			float current = Mathf.Clamp(curr, 0,1);
				Debug.Log ("setting trans to : " + current);
			mat.SetColor ("_Color", new Color(1.0f, 1.0f, 1.0f, current));
		}
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 offset = new Vector2(Random.value * offsetScale, Random.value * offsetScale);
		//rend.material.mainTextureOffset = offset;
		mat.mainTextureOffset = offset;

	}
}
