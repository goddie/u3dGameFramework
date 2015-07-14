using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("a");
		StartCoroutine(PlayOneShot());
		Debug.Log("b");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator PlayOneShot ()
	{
		Debug.Log("1");
		yield return new WaitForSeconds(2);
		Debug.Log("2");
		yield return new WaitForSeconds(2);
		Debug.Log("3");
	}



}
