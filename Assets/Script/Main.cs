using System;
using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	private GameObject btn1;
	private GameObject btn2;
	private GameObject btn3;
	private GameObject btnStart;

	void Start ()
	{
		Application.targetFrameRate = 30;

		GlobalConfig.GetInstance.InitGlobalSetting ();

		btn1 = GameObject.Find ("btnHF");
		btn2 = GameObject.Find ("btnOD");
		btn3 = GameObject.Find ("btnODSpell");
		btnStart = GameObject.Find ("btnStart");

		UUIEventListener.Get (btn1).onClick = btn1ClickHandler;
		UUIEventListener.Get (btn2).onClick = btn2ClickHandler;
		UUIEventListener.Get (btn3).onClick = btn3ClickHandler;
		UUIEventListener.Get (btnStart).onClick = btnStartClickHandler;
		//MessageCenter.GetInstance.addEventListener (BaseEvent.CLICK, btn1ClickHandler);
		//MainComponentManager main = MainComponentManager.SharedInstance;
		
//		StartCoroutine (DelayToInvokeDo (2.0f));
	}

	void Update ()
	{

	}

	public IEnumerator DelayToInvokeDo (float delaySeconds)
	{
		yield return new WaitForSeconds (delaySeconds);

	}


	void btn1ClickHandler (GameObject go)
	{


//		Debug.Log ("btn1ClickHandler");

//		AttackMessage message = new AttackMessage ();
		
		EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 1);
	}

	void btn2ClickHandler (GameObject go)
	{
		//Debug.Log ("btn2ClickHandler");

		EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 2);
	}

	void btn3ClickHandler (GameObject go)
	{
		EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 3);
	}


	void btnStartClickHandler (GameObject go)
	{
		//Debug.Log("btnStartClickHandler");
		BattleManager.GetInstance.BattleStart();
	}


}
