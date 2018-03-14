using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallOnPickup : MonoBehaviour {

    Rigidbody myrb;

	// Use this for initialization
	void Start () {
        myrb = GetComponent<Rigidbody>();
        myrb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myrb.isKinematic = false;
        }
	}
}
