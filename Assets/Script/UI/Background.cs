using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Background : MonoBehaviour
{



	void Start ()
	{

		InvokeRepeating ("Move", 3, 5.0f);
		//InvokeRepeating ("AddFlash", 0, 5.0f);
	}

	protected virtual void Move ()
	{

		StartCoroutine (MoveTopBottom ());
		StartCoroutine (FlashCloud ());
 
	}


	void AddFlash()
	{
		GameObject parent = StageManager.SharedInstance.EffectLayer;
		GameObject flash = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [29].Prefab);
		GameObject popo2 = StageManager.SharedInstance.AddToStage (gameObject, flash);

		if (flash.GetComponent<BaseEvent>()==null) {
			flash.AddComponent<BaseEffect>();
		}


		popo2.gameObject.transform.position = MapUtil.GetInstance.MapToWorld(10,6);
	}

	/// <summary>
	/// 从左到右移动
	/// </summary>
	/// <param name="time">Time.</param>
	protected virtual IEnumerator MoveLeftRight()
	{

		Transform layer2 = GetTransform (gameObject.transform, "layer2");
		GameObject go = layer2.gameObject;


		Hashtable args = new Hashtable ();
		args.Add ("position", new Vector3 (go.transform.position.x + 2.0f, go.transform.position.y, go.transform.position.z));
		args.Add ("time", 2.0f);
		iTween.MoveTo (go, args);

		yield return new WaitForSeconds(0);
	}

	IEnumerator FlashCloud ()
	{


		Transform layer2 = GetTransform (gameObject.transform, "layer4");
		GameObject go = layer2.gameObject;

		Image image = go.GetComponent<Image> ();
			
		Color origin = image.color;

		Color c = new Color ();
		c.r = 240.0f / 255.0f;
		c.g = 240.0f / 255.0f;
		c.b = 240.0f / 255.0f;
		c.a = 1.0f;

		image.color = c;

		yield return new WaitForSeconds (0.1f);
		image.color = origin;

		yield return new WaitForSeconds (0.1f);
		image.color = c;

		yield return new WaitForSeconds (0.1f);
		image.color = origin;
	}

	/// <summary>
	/// 从左到右移动
	/// </summary>
	/// <param name="time">Time.</param>
	protected virtual IEnumerator MoveTopBottom ()
	{
		float time = 2.0f;

		Transform layer2 = GetTransform (gameObject.transform, "layer2");
		GameObject go = layer2.gameObject;
	

		Vector3 origin = go.transform.position;

		Vector3 pos = new Vector3 (go.transform.position.x, go.transform.position.y - 1.5f, go.transform.position.z);
  

		Hashtable args = new Hashtable ();
		args.Add ("time", 2.0f);
		args.Add ("position", pos);
		args.Add ("easetype", "linear");
		iTween.MoveTo (go, args);

		yield return new WaitForSeconds (1.0f);
		//Vector3 pos1 = new Vector3 (go.transform.position.x, go.transform.position.y + 0.3f, go.transform.position.z);


		args.Clear ();

		args.Add ("time", 2.0f);
		args.Add ("position", origin);
		args.Add ("easetype", "linear");

		iTween.MoveTo (go, args);


		Transform layer3 = GetTransform (gameObject.transform, "layer3");
		GameObject go2 = layer3.gameObject;
		
		
		Vector3 origin2 = go2.transform.position;
		Vector3 pos2 = new Vector3 (go2.transform.position.x, go2.transform.position.y - 0.5f, go2.transform.position.z);

		args.Clear ();
		args.Add ("time", 1.0f);
		args.Add ("position", pos2);
		args.Add ("easetype", "linear");
		iTween.MoveTo (go2, args);
		
		yield return new WaitForSeconds (1.0f);
		//Vector3 pos1 = new Vector3 (go.transform.position.x, go.transform.position.y + 0.3f, go.transform.position.z);
		
		
		args.Clear ();
		
		args.Add ("time", 1.0f);
		args.Add ("position", origin2);
		args.Add ("easetype", "linear");
		
		iTween.MoveTo (go2, args);
	}

	protected Transform GetTransform (Transform check, string name)
	{
		Transform[] list = check.GetComponentsInChildren<Transform> ();

		for (int i=0; i<list.Length; i++) {
			Transform t = list [i];
			if (t.name == name) {
				return t;
			}
			//GetTransform (t, name);
		}
		return null;
	}
}
