using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchOrientation : MonoBehaviour {
	public float Scale = .3f;
	public Transform target;
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Slerp(transform.localRotation, target.localRotation, Scale);
	}
}
