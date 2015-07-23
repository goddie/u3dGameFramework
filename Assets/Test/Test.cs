using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		addGrid ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnGUI ()
	{
//		if (Input.GetMouseButtonDown (0)) {
//
//			Debug.Log ("Input.mousePosition:" + Input.mousePosition);
//		}



	}

	void addGrid ()
	{
		int w = Screen.width / 50;
		int h = Screen.height / 20;

		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
					


			}
		}
	}

	void getRout ()
	{



		route_pt[] result = null;
		// result_pt是一个简单类，它只有两个成员变量：int x和int y。
		// 参数说明：map是一个二维数组，具体见程序注释
		AStarRoute asr = new AStarRoute (new int[100, 100], 0, 0, 5, 5);

		try {
			result = asr.getResult ();
		} catch (Exception ex) {

		}

		Debug.Log (result);
	}

}
