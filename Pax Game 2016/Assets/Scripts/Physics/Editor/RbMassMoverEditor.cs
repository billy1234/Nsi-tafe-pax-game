using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RbMassMover))]
public class RbMassMoverEditor : Editor {

	public override void OnInspectorGUI()
	{
		RbMassMover r = target as RbMassMover;
		DrawDefaultInspector ();
		if(GUILayout.Button("Set Center"))
		{
			r.changeRbMass ();
		}
	}


}
