using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyParentOnTigger : MonoBehaviour {

	private IReceiveTriggerFromChildren _receiver;

	void Awake()
	{
		_receiver = GetComponentInParent<IReceiveTriggerFromChildren>();
	}
	void OnTriggerEnter(Collider collider)
	{
		_receiver.OnChildTriggerEnter(collider);
	}

	void OnTriggerStay(Collider collider)
	{
		_receiver.OnChildTriggerStay(collider);
	}

	void OnTriggerExit(Collider collider)
	{
		_receiver.OnChildTriggerExit(collider);
	}
}
