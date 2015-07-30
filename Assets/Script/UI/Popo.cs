using System;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 对话泡泡
/// </summary>
public class Popo : MonoBehaviour
{

	private Text txt;

	void Awake()
	{
		GameObject go = GameObject.Find("txt");
		txt = go.GetComponent<Text>();

	}

	public void SetText(string text)
	{
		txt.text = text;
	}


}
