using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (FieldOfVieuw))]
public class FieldOfViewEditor : Editor {
	void OnSceneGUI(){
		FieldOfVieuw fow = (FieldOfVieuw)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector2.up, 360, fow.viewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in fow.visibleTargets){
			Handles.DrawLine(fow.transform.position, visibleTarget.position);
		}
	}
}
