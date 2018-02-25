using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiveTriggerFromChildren {

	void OnChildTriggerEnter(Collider col);
	void OnChildTriggerExit(Collider col);
	void OnChildTriggerStay(Collider col);
}
