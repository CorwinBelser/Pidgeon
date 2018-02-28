using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotationsInHeirarchy : MonoBehaviour {
	public Transform mirror;

	// Update is called once per frame
	void Update () {
		if (mirror != null)
			transform.localRotation = mirror.localRotation;
	}
}
