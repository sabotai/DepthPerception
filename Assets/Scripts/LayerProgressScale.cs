using UnityEngine;
using System.Collections;

public class LayerProgressScale : MonoBehaviour {

	private int currentState;
	public Renderer rend;
	public Material mat;
	public float alphaMult = 5;

	// Use this for initialization
	void Start () {
		currentState = PlayerPrefs.GetInt("layerProgress");
		//rend = GetComponent<Renderer> ();
		mat.SetColor ("_Color", new Color(1.0f, 1.0f, 1.0f, (currentState * alphaMult)/100f));
	}
	
	// Update is called once per frame
	void Update () {
		//rend.material.color.a  = currentState * alphaMult;
	}
}
