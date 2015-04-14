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
		mat.SetColor ("_Color", new Color(1.0f, 1.0f, 1.0f, (currentLayer * alphaMultiplier)/100f));
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 offset = new Vector2(Random.value * offsetScale, Random.value * offsetScale);
		//rend.material.mainTextureOffset = offset;
		mat.mainTextureOffset = offset;

	}
}
