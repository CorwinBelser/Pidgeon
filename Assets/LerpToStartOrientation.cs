using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToStartOrientation : MonoBehaviour {
	public Vector3 startAngles;
	public float lerpSpeed = 1f;


	// Use this for initialization
	void Start () {
		startAngles = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped (transform.localEulerAngles, startAngles, lerpSpeed * Time.deltaTime));
	}
}
