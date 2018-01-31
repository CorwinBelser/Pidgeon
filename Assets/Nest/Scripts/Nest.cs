using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour {

    private List<Transform> _collectibles;

	// Use this for initialization
	void Start () {
        _collectibles = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == Collectible.COLLECTIBLE_TAG)
        {
            /* Freeze the object in the nest */
            coll.GetComponent<Rigidbody>().isKinematic = true;
            _collectibles.Add(coll.transform);
        }
    }
}
