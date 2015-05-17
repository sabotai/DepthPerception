﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	//script credit:http://mecwarriors.com/2013/11/22/humanoid-animation-free-root-motion/

	public bool useAdditiveRotation;
	Transform mainCamera;      //holds the main camera as a Transform object
	Animator animController;   //holds the player character's animator controller

	//holds the player's current enumerated movement state
	MovementState currentMovementState;

	//the character's possible movement states
	enum MovementState
	{
		Idle = 0,
		Walking = 1,
		Running = 2
	}

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main.transform;           //initialize the camera's transform
		animController = GetComponent<Animator>();    //initialize the animator controller
	}
	void Update () {
		//get axis input from player (W, A, S, and D keys/Arrow keys)
		float verticalAxis = Input.GetAxis("Vertical");
		float horizontalAxis = Input.GetAxis("Horizontal");
		float verticalAxisJ = Input.GetAxis("VerticalJ");
		float horizontalAxisJ = Input.GetAxis("HorizontalJ");

		//get the forward-facing direction of the camera
		Vector3 cameraForward = mainCamera.TransformDirection(Vector3.forward);
		cameraForward.y = 0;    //set to 0 because of camera rotation on the X axis
		
		//get the right-facing direction of the camera
		Vector3 cameraRight = mainCamera.TransformDirection(Vector3.right);

		//determine the direction the player will face based on input and the camera's right and forward directions
		Vector3 targetDirection = horizontalAxis * cameraRight + verticalAxis * cameraForward;
		
		//normalize the direction the player should face
		Vector3 lookDirection = targetDirection.normalized;

		//rotate the player to face the correct direction ONLY if there is any player input
		if (lookDirection != Vector3.zero){
			if (useAdditiveRotation){
				/*
				Vector3 temp = lookDirection - Vector3.Normalize(transform.rotation.eulerAngles);
				Debug.Log (lookDirection + " vs " + temp);
				transform.rotation = Quaternion.Euler(temp);
				*/
			} else {

				transform.rotation = Quaternion.LookRotation(lookDirection);
			}
		}

		//if there is any player input...
		if (verticalAxis != 0 || horizontalAxis != 0 || verticalAxisJ != 0 || horizontalAxisJ != 0)
		{
			//if the run modifier key is pressed
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				currentMovementState = MovementState.Running;      //set movement state to running
			else    //if there is no movement modifier
				currentMovementState = MovementState.Walking;      //set movement state to walking
		}
		else    //default to idle if no input
		{
			currentMovementState = MovementState.Idle;    //set movement state to idle
		}

		//send the current movement state to the character controller
		animController.SetInteger("MovementState", (int)currentMovementState);

	}
}
