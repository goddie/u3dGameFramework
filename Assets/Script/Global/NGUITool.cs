using System;
using UnityEngine;

public static class NGUITool
{
	/// <summary>
	/// Add a new child game object.
	/// </summary>

	static public GameObject AddChild (GameObject parent)
	{
		return AddChild (parent, true);
	}

	/// <summary>
	/// Add a new child game object.
	/// </summary>

	static public GameObject AddChild (GameObject parent, bool undo)
	{
		GameObject go = new GameObject ();
		#if UNITY_EDITOR
		if (undo)
			UnityEditor.Undo.RegisterCreatedObjectUndo (go, "Create Object");
		#endif
		if (parent != null) {
			Transform t = go.transform;
			t.SetParent (parent.transform, false);
			//t.parent = parent.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
			go.layer = parent.layer;
		}
		return go;
	}

	/// <summary>
	/// Instantiate an object and add it to the specified parent.
	/// </summary>

	static public GameObject AddChild (GameObject parent, GameObject prefab)
	{
		GameObject go = GameObject.Instantiate (prefab) as GameObject;
		#if UNITY_EDITOR
		UnityEditor.Undo.RegisterCreatedObjectUndo (go, "Create Object");
		#endif
		if (go != null && parent != null) {
			Transform t = go.transform;

			t.SetParent (parent.transform, false);
			//t.parent = parent.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
			go.layer = parent.layer;
			//go.name = go.name.Replace ("(clone)", "");
		}
		return go;
	}
}
