using UnityEngine;
using System.Collections;

public class SobelEdge01 : MonoBehaviour {
	//Private Variables//
	private Shader shader;
	private bool button;

	//Mono Methods//
	void Awake () {
		//Set the appropriate replacement shader
		shader = Shader.Find("Aubergine/Replacement/SobelEdge01");
	}
	
	void Update () {
		if (Input.GetKeyDown("space")) {
			button = !button;
			if (button) {
				transform.GetComponent<Camera>().SetReplacementShader(shader, null);
			}
			else
				transform.GetComponent<Camera>().ResetReplacementShader();
		}
	}
}