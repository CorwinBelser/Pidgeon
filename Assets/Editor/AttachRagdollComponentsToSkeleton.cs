using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HingeJointTargetDistributor))]
class HingeJointTargetDistributorEditor : Editor {
	SerializedProperty _mirror;

	void OnEnable()
	{
		_mirror = serializedObject.FindProperty("_mirror");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		EditorGUILayout.PropertyField(_mirror);
		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button ("AddScriptToChildren")) {
			HingeJointTargetDistributor go = (HingeJointTargetDistributor)target;
			HingeJointTarget hjt;

			if (go.GetComponent<HingeJointTarget> () == null) {
				hjt = go.gameObject.AddComponent<HingeJointTarget> ();
			} else {
				hjt = go.GetComponent<HingeJointTarget> ();
			}
			hjt.target = (Transform)_mirror.objectReferenceValue;
			HingeJoint newHingeJoint;
			if (hjt.gameObject.GetComponent<HingeJoint> () == null) {
				newHingeJoint = hjt.gameObject.AddComponent<HingeJoint> ();
			} else {
				newHingeJoint = hjt.gameObject.GetComponent<HingeJoint> ();
			}

			hjt.hj = newHingeJoint;
			AddMatchRotationsToChildren (hjt.transform, hjt.target);
		}
	}

	public void AddMatchRotationsToChildren(Transform root, Transform mirror) {
		for (int i = 0; i < root.childCount; i++) {
			if (root.GetChild (i).childCount > 0) {
				HingeJointTarget newComponent;
				if (root.GetChild (i).GetComponent<HingeJointTarget> () == null) {
					newComponent = root.GetChild (i).gameObject.AddComponent<HingeJointTarget> ();
				} else {
					newComponent = root.GetChild (i).gameObject.GetComponent<HingeJointTarget> ();
				}
				newComponent.z = true;
				HingeJoint newHingeJoint;
				if (root.GetChild (i).GetComponent<HingeJoint> () == null) {
					newHingeJoint = root.GetChild (i).gameObject.AddComponent<HingeJoint> ();
				} else {
					newHingeJoint = root.GetChild (i).gameObject.GetComponent<HingeJoint> ();
				}
				newComponent.target = mirror.GetChild (i);
				newComponent.hj = newHingeJoint;
				JointLimits limits = newHingeJoint.limits;
				limits.min = -180;
				limits.max = 180;
				newHingeJoint.limits = limits;
				JointSpring spring = newHingeJoint.spring;
				spring.spring = 20;
				newHingeJoint.spring = spring;
				newHingeJoint.useSpring = true;

			}
			root.gameObject.GetComponent<HingeJoint> ().connectedBody = root.GetChild (0).GetComponent<Rigidbody> ();
			if (root.GetChild (i).GetComponent<Rigidbody> () == null) {
				root.GetChild (i).gameObject.AddComponent<Rigidbody> ();
			}

			root.GetChild (i).GetComponent<Rigidbody> ().useGravity = false;
			root.GetChild (i).GetComponent<Rigidbody> ().mass = .05f;

			AddMatchRotationsToChildren (root.GetChild (i), mirror.GetChild (i));

		}
	}
}
	