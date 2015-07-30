 
using System;
using UnityEngine;


public class TestTouch : MonoBehaviour
{
	public TestTouch ()
	{
	}

	void Update ()
	{
		
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
 
			Debug.Log ("SlashScreen");
		}
		
	}
}