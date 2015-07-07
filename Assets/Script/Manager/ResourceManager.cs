using System;
using UnityEngine;

public class ResourceManager
{
	private static ResourceManager instance;

	private ResourceManager ()
	{
	}

	public static ResourceManager GetInstance {
		get {
			if (instance == null) {

				instance = new ResourceManager ();
			}
			return instance;
		}
	}


	public GameObject LoadPrefab (string resPath)
	{
		GameObject go = Resources.Load (resPath) as GameObject;
		return go;
	}
}
 
