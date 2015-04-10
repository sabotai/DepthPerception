using UnityEngine;
using System.Collections;

public class RandomTextureOffset : MonoBehaviour {

	public Renderer rend;
	public float scale = 5f;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(Random.value * scale, Random.value * scale);
		rend.material.mainTextureOffset = offset;
	}
}
