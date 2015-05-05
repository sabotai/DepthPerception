using UnityEngine;
using System.Collections;

public class KinectEnable : MonoBehaviour {


	private GameObject kinectObj;
	private int previousCount, currentCount;
	// Use this for initialization
	void Start () {
		kinectObj = GameObject.Find("KinectPrefab");
		previousCount = 0;
		currentCount = kinectObj.GetComponent<SkeletonWrapper>().trackedCount;
	}
	
	// Update is called once per frame
	void Update () {
		currentCount = kinectObj.GetComponent<SkeletonWrapper>().trackedCount;
		if (currentCount != previousCount){
			GameObject[] puppets = GameObject.FindGameObjectsWithTag("kinectEnable");
			Debug.Log ("found " + puppets.Length + " puppets");
			for (int i = 0; i<puppets.Length; i++){
				if (currentCount > previousCount){
					Debug.Log ("enabling puppets");
					puppets[i].GetComponent<SkinnedMeshRenderer>().enabled = true;
				}
				if (currentCount < previousCount){
					Debug.Log ("disabling puppets");
					puppets[i].GetComponent<SkinnedMeshRenderer>().enabled = false;
				}
			}
		}
		previousCount = currentCount;
	}
}
