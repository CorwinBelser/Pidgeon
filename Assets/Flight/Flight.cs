using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour {
	private Rigidbody muhBody;
	public float defaultForwardSpeed = 5f;
	public float pitchSensitivity = 30f;
	public float rollSensitivity = 45f;

//	private float targetPitch = 0f;
//	private float targetRoll = 0f;
	// Use this for initialization
	void Start () {
		muhBody = GetComponent<Rigidbody> ();
		muhBody.velocity = new Vector3 (0, 0, defaultForwardSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		float pitchAdjust = 0f;
		float rollAdjust = 0f;
		if (Input.GetKey (KeyCode.UpArrow)) {
			pitchAdjust += pitchSensitivity;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			pitchAdjust -= pitchSensitivity;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rollAdjust += rollSensitivity;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rollAdjust -= rollSensitivity;
		}
		pitchAdjust *= Time.deltaTime;
		rollAdjust *= Time.deltaTime;
//		targetPitch += pitchAdjust;
//		targetRoll += rollAdjust;
//		transform.rotation = Quaternion.Euler (0f, 0f, targetRoll);
//		transform.rotation *= Quaternion.AngleAxis (targetPitch, transform.right);
//
//		muhBody.velocity = transform.forward * defaultForwardSpeed;
		muhBody.AddTorque(transform.up * rollAdjust);
		muhBody.AddTorque(transform.right * pitchAdjust);
		float currentZ = transform.localRotation.eulerAngles.z;
		if (currentZ > 180f) {
			currentZ = -(360 - currentZ);
		}

		if (currentZ > 70f)
		{
			Debug.Log(transform.localRotation.eulerAngles.ToString());
			Vector3 newRot = transform.localRotation.eulerAngles;
			newRot.z = 70f;
			transform.localRotation = Quaternion.Euler(newRot);

		}
		else if (currentZ < -70f)
		{
			Debug.Log(transform.localRotation.eulerAngles.ToString());
			Vector3 newRot = transform.localRotation.eulerAngles;
			newRot.z = -70f;
			transform.localRotation = Quaternion.Euler(newRot);

		}


		float currentX = transform.localRotation.eulerAngles.y;
		if (currentX > 180f) {
			currentX = -(360 - currentX);
		}

		if (currentX > 70f)
		{
			Debug.Log(transform.localRotation.eulerAngles.ToString());
			Vector3 newRot = transform.localRotation.eulerAngles;
			newRot.y = 70f;
			transform.localRotation = Quaternion.Euler(newRot);

		}
		else if (currentX < -70f)
		{
			Debug.Log(transform.localRotation.eulerAngles.ToString());
			Vector3 newRot = transform.localRotation.eulerAngles;
			newRot.y = -70f;
			transform.localRotation = Quaternion.Euler(newRot);

		}
	}
}
