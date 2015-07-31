using System;
using System.Collections;
using UnityEngine;

public class BackgroundSky: Background
{
	void Start ()
	{

		//InvokeRepeating ("Move2", 0, 4.5f);
		InvokeRepeating ("Move", 0, 4.0f);

		Move2();
	}

	protected override void Move ()
	{
		
		StartCoroutine (MoveTopBottom ());
		
	}

	protected void Move2()
	{
		StartCoroutine (MoveLeftRight ());
	}


	/// <summary>
	/// 从左到右移动
	/// </summary>
	/// <param name="time">Time.</param>
	protected override IEnumerator MoveLeftRight ()
	{

		//Vector3 pos1 = new Vector3 (go.transform.position.x, go.transform.position.y + 0.3f, go.transform.position.z);
		Hashtable args = new Hashtable ();

		Transform layer3 = GetTransform (gameObject.transform, "layer3");
		GameObject go = layer3.gameObject;
		
		
		Vector3 origin2 = go.transform.position;
		Vector3 pos2 = new Vector3 (go.transform.position.x - 0.3f, go.transform.position.y, go.transform.position.z);
		
		args.Clear ();
		args.Add ("time", 1.5f);
		args.Add ("position", pos2);
		args.Add ("easetype", "linear");
		iTween.MoveTo (go, args);
		
		yield return new WaitForSeconds (1.5f);
		//Vector3 pos1 = new Vector3 (go.transform.position.x - 0.5f, go.transform.position.y, go.transform.position.z);
		
		
		args.Clear ();
		
		args.Add ("time", 1.5f);
		args.Add ("position", origin2);
		args.Add ("easetype", "linear");
		
		iTween.MoveTo (go, args);
		
		yield return new WaitForSeconds (1.5f);
		Vector3 pos1 = new Vector3 (go.transform.position.x + 0.3f, go.transform.position.y, go.transform.position.z);
		
		args.Clear ();
		
		args.Add ("time", 1.5f);
		args.Add ("position", pos1);
		args.Add ("easetype", "linear");
		
		iTween.MoveTo (go, args);



		yield return new WaitForSeconds (1.5f);

		args.Clear ();
		
		args.Add ("time", 1.5f);
		args.Add ("position", origin2);
		args.Add ("easetype", "linear");
		
		iTween.MoveTo (go, args);


		yield return new WaitForSeconds (1.5f);

		Move2();
	}

	/// <summary>
	/// 从左到右移动
	/// </summary>
	/// <param name="time">Time.</param>
	protected override IEnumerator MoveTopBottom ()
	{
		float time = 2.0f;
		
		Transform layer2 = GetTransform (gameObject.transform, "layer2");
		GameObject go = layer2.gameObject;
		
		
		Vector3 origin = go.transform.position;
		
		Vector3 pos = new Vector3 (go.transform.position.x, go.transform.position.y - 0.5f, go.transform.position.z);
		
		
		Hashtable args = new Hashtable ();
		args.Add ("time", 2.0f);
		args.Add ("position", pos);
		args.Add ("easetype", "linear");
		iTween.MoveTo (go, args);
		yield return new WaitForSeconds (2.0f);

		args.Clear();
		args.Add ("time", 2.0f);
		args.Add ("position", origin);
		args.Add ("easetype", "linear");
		iTween.MoveTo (go, args);
	}
}