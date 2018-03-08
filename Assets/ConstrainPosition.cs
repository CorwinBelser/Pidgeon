using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainPosition : MonoBehaviour {
	public bool x, y, z;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		if (x)
			localVelocity.x = 0;
		if (y)
			localVelocity.y = 0;
		if (z)
			localVelocity.z = 0;
		
		GetComponent<Rigidbody>().velocity = transform.TransformDirection(localVelocity);
	}
}
