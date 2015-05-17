using UnityEngine;
using System.Collections;

public class joystick : MonoBehaviour {

	public float sensitivityX = 3f;
	private float rotationX;
		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetAxis("HorizontalJ") != 0){

			rotationX = transform.localEulerAngles.y + Input.GetAxis("HorizontalJ") * sensitivityX;
	
			
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, 0);
		
		}
	
	}
}
