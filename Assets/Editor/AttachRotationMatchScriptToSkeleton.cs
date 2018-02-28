using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MatchRotationsInHeirarchy))]
class MatchRotationsInHeirarchyEditor : Editor {
	SerializedProperty mirror;

	void OnEnable()
	{
		mirror = serializedObject.FindProperty("mirror");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		EditorGUILayout.PropertyField(mirror);
		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button ("AddScriptToChildren")) {
			MatchRotationsInHeirarchy mrih = (MatchRotationsInHeirarchy)target;
			AddMatchRotationsToChildren (mrih.transform, mrih.mirror);
		}
	}

	public void AddMatchRotationsToChildren(Transform root, Transform mirror) {
		for (int i = 0; i < root.childCount; i++) {
			MatchRotationsInHeirarchy newComponent;
			if (root.GetChild (i).GetComponent<MatchRotationsInHeirarchy> () == null) {
				newComponent = root.GetChild (i).gameObject.AddComponent<MatchRotationsInHeirarchy> ();
			} else {
				newComponent = root.GetChild (i).gameObject.GetComponent<MatchRotationsInHeirarchy> ();
			}
			newComponent.mirror = mirror.GetChild (i);
			AddMatchRotationsToChildren (root.GetChild (i), mirror.GetChild (i));
		}
	}
}
	