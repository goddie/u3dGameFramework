using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	private GameObject btnHFAttack;
	private GameObject btnODAttack;
	private GameObject btnODUlt;
	private GameObject btnStart;

	private GameObject btnMXAttack;
	private GameObject btnMXUlt;

	private GameObject btnLEAttack;
	private GameObject btnLEUlt;

	private GameObject btnHMAttack;
	private GameObject btnHMUlt;

	private GameObject btnRRAttack;
	private GameObject btnRRUlt;

	void Start ()
	{
		//MapUtil.GetInstance.DrawGrid ();

		Application.targetFrameRate = 30;

		GlobalConfig.GetInstance.InitGlobalSetting ();

		GameObject.Find ("frame1").SetActive(false);
		GameObject.Find ("frame2").SetActive(false);
		GameObject.Find ("frame3").SetActive(false);
		GameObject.Find ("frame4").SetActive(false);
		GameObject.Find ("frame5").SetActive(false);


		btnHFAttack = GameObject.Find ("btnHFAttack");

		btnStart = GameObject.Find ("btnStart");

		btnMXUlt = GameObject.Find ("btnMX");
		btnLEUlt = GameObject.Find ("btnLE");
		btnHMUlt = GameObject.Find ("btnHM");
		btnODUlt = GameObject.Find ("btnOD");
		btnRRUlt = GameObject.Find ("btnRR");

//		UUIEventListener.Get (btnHFAttack).onClick = btn1ClickHandler;
//
//		
//		UUIEventListener.Get (btnODAttack).onClick = btnAttackHandler;
//		UUIEventListener.Get (btnMXAttack).onClick = btnAttackHandler;
//		UUIEventListener.Get (btnLEAttack).onClick = btnAttackHandler;
//		UUIEventListener.Get (btnHMAttack).onClick = btnAttackHandler;

		
		UUIEventListener.Get (btnODUlt).onClick = btnUtlHandler;
		UUIEventListener.Get (btnMXUlt).onClick = btnUtlHandler;
		UUIEventListener.Get (btnLEUlt).onClick = btnUtlHandler;
		UUIEventListener.Get (btnHMUlt).onClick = btnUtlHandler;
		UUIEventListener.Get (btnRRUlt).onClick = btnUtlHandler;

		UUIEventListener.Get (btnStart).onClick = btnStartClickHandler;
		//MessageCenter.GetInstance.addEventListener (BaseEvent.CLICK, btn1ClickHandler);
		MainComponentManager main = MainComponentManager.SharedInstance;
		
//		StartCoroutine (DelayToInvokeDo (2.0f));
	}

	void Update ()
	{

	}

	public IEnumerator DelayToInvokeDo (float delaySeconds)
	{
		yield return new WaitForSeconds (delaySeconds);

	}

	/// <summary>
	///  攻击按钮
	/// </summary>
	/// <param name="go">Go.</param>
	void btnAttackHandler (GameObject go)
	{
		Text txt = go.GetComponentInChildren<Text> ();
		//Text txt =btn.GetComponentInChildren<Text>();
		//Debug.Log(txt.text);

		if (txt.text.IndexOf ("奥丁") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 1);
		}

		if (txt.text.IndexOf ("绿萼") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 2);
		}

		if (txt.text.IndexOf ("慕雪") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 3);
		}

		if (txt.text.IndexOf ("寒梦") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 4);
		}
		

	}

	/// <summary>
	/// 大招按钮
	/// </summary>
	/// <param name="go">Go.</param>
	void btnUtlHandler (GameObject go)
	{
	
		string name = go.name;
		//Text txt =btn.GetComponentInChildren<Text>();
		//Debug.Log(txt.text);
		
		if (name.IndexOf ("OD") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ULT, 11);
		}

		if (name.IndexOf ("LE") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ULT, 21);
		}

		if (name.IndexOf ("MX") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ULT, 31);
		}

		if (name.IndexOf ("HM") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ULT, 41);
		}

		if (name.IndexOf ("RR") >= 0) {
			EventCenter.GetInstance.dispatchEvent (BattleEvent.ULT, 51);
		}
	}


	void btn1ClickHandler (GameObject go)
	{
		
//		Debug.Log ("btn1ClickHandler");
//		AttackMessage message = new AttackMessage ();
		EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 0);
	}


	void btnStartClickHandler (GameObject go)
	{
		//Debug.Log("btnStartClickHandler");
		BattleManager.SharedInstance.BattleStart ();
	}


}
