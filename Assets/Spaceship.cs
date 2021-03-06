﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {
	
	public Collectible COLLECTIBLE;
	public float SPEED;
	private Rigidbody _rb;
	private float _forward;
	private float _right;
	private float _up;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody>();
		_forward = 1;
		_right = 0;
		_up = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(COLLECTIBLE.GetTarget() == null)
			Fly();
	}
	
	private void Fly(){
		_forward = _forward;
		_right = Mathf.Clamp(Random.Range(_right-0.01f,_right+0.01f),-0.1f,0.1f);
		_up = Mathf.Clamp(Random.Range(_up-0.01f,_up+0.01f),-0.1f,0.1f);
		//_rb.velocity = new Vector3(_horizontal,_vertical,5);
		
		

		Vector3 forward = transform.forward;
		forward.Normalize();
		Vector3 right = transform.right;
		right.Normalize();
		Vector3 up = transform.up;
		up.Normalize();

		Vector3 direction = forward * _forward * SPEED + right * _right + up * _up;
		_rb.velocity = direction;
		transform.rotation=Quaternion.LookRotation(_rb.velocity);
	}
}
