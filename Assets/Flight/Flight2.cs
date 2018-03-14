using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight2 : MonoBehaviour {
	private Rigidbody muhBody;
	public float defaultForwardSpeed = 5f;
	public float pitchSensitivity = 15f;
	public float rollSensitivity = 15f;
	public float gravityMod = 3f;
	public float positionYScale;
	public float positionXScale;

	private float currentV = 0f;
	private float currentH = 0f;

	public Transform bird;

//	private float targetPitch = 0f;
//	private float targetRoll = 0f;
	// Use this for initialization
	void Start () {
		muhBody = GetComponent<Rigidbody> ();
		muhBody.velocity = new Vector3 (0, 0, defaultForwardSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		float vAdjust = 0f;
		float hAdjust = 0f;
		if (Input.GetKey (KeyCode.W)) {
			vAdjust += pitchSensitivity;
		}
		if (Input.GetKey (KeyCode.S)) {
			vAdjust -= pitchSensitivity;
		}
		if (Input.GetKey (KeyCode.A)) {
			hAdjust += rollSensitivity;
		}
		if (Input.GetKey (KeyCode.D)) {
			hAdjust -= rollSensitivity;
		}
		vAdjust *= Time.deltaTime;
		hAdjust *= Time.deltaTime;

		currentV += vAdjust;
		currentH += hAdjust;

		if (currentV > 5) {
			currentV = 5;
		}else if (currentV < -5) {
			currentV = -5;
		} 

//		if (currentH > 5) {
//			currentH = 5;
//		}else if (currentH < -5) {
//			currentH = -5;
//		} 

		transform.localRotation = Quaternion.AngleAxis (vAdjust, transform.right) * transform.localRotation;
		transform.rotation = Quaternion.AngleAxis (hAdjust, Vector3.up) * transform.rotation;

//		Vector3 targetDirection = (transform.forward * defaultForwardSpeed) + (transform.up * currentV) + (transform.right * currentH);
		muhBody.velocity = transform.forward * (defaultForwardSpeed + gravityMod * Vector3.Dot(transform.forward, Vector3.down));

//		Quaternion targetRotation = Quaternion.AngleAxis (muhBody.velocity.y * 20f, bird.right);
		Quaternion targetRotation = Quaternion.AngleAxis (-hAdjust * 10f, Vector3.forward);
		bird.localRotation = Quaternion.SlerpUnclamped (bird.localRotation, targetRotation, Time.deltaTime * 3f);

		Vector3 targetLocalPosition = (muhBody.velocity.y * Vector3.up * positionYScale) + (-hAdjust * Vector3.right * positionXScale);
		bird.localPosition = Vector3.SlerpUnclamped (bird.localPosition, targetLocalPosition, Time.deltaTime * 3f);

//		bird.rotation = Quaternion.AngleAxis (hAdjust, bird.forward) * transform.rotation;
			
//		transform.LookAt (transform.position + targetDirection);
	}
}
