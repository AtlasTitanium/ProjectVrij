using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfVieuw))]
public class FieldOfViewEditor : Editor {
	void OnSceneGUI(){
		FieldOfVieuw fow = (FieldOfVieuw)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
	}
}
