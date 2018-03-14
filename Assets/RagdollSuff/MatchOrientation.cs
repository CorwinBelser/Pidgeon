using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchOrientation : MonoBehaviour {
	
	public Transform target;
	public bool x,y,z,local,instant;
	public float slerpScale = .3f;

	// Update is called once per frame
	void Update () {
		if (!local) {
			Vector3 newRotation = transform.eulerAngles;
			if (x) {
				newRotation.x = target.eulerAngles.x;
			}
			if (y) {
				newRotation.y = target.eulerAngles.y;
			}
			if (z) {
				newRotation.z = target.eulerAngles.z;
			}

			if (instant) {
				transform.rotation = Quaternion.Euler (newRotation);
			} else {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (newRotation), slerpScale);
			}
		} else {
			Vector3 newRotation = transform.localEulerAngles;
			if (x) {
				newRotation.x = target.localEulerAngles.x;
			}
			if (y) {
				newRotation.y = target.localEulerAngles.y;
			}
			if (z) {
				newRotation.z = target.localEulerAngles.z;
			}
			if (instant) {
				transform.localRotation = Quaternion.Euler (newRotation);
			} else {
//				transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.Euler (newRotation), slerpScale);
			}
		}
	}
}
