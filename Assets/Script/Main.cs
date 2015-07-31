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
	private GameObject loadingImage;
	private GameObject btnTimer;
 

	void Start ()
	{
		//MapUtil.GetInstance.DrawGrid ();

		Application.targetFrameRate = 30;

		GlobalConfig.GetInstance.InitGlobalSetting ();
 

//		GameObject.Find ("frame1").SetActive (false);
//		GameObject.Find ("frame2").SetActive (false);
//		GameObject.Find ("frame3").SetActive (false);
//		GameObject.Find ("frame4").SetActive (false);
//		GameObject.Find ("frame5").SetActive (false);

		loadingImage = GameObject.Find ("Loading");
		btnHFAttack = GameObject.Find ("btnHFAttack");

		btnStart = GameObject.Find ("btnStart");

		btnMXUlt = GameObject.Find ("btnMX");
		btnLEUlt = GameObject.Find ("btnLE");
		btnHMUlt = GameObject.Find ("btnHM");
		btnODUlt = GameObject.Find ("btnOD");
		btnRRUlt = GameObject.Find ("btnRR");
		btnTimer = GameObject.Find	("btnTimer");

//		UUIEventListener.Get (btnHFAttack).onClick = btn1ClickHandler;
//
//		
//		UUIEventListener.Get (btnODAttack).onClick = btnAttackHandler;
//		UUIEventListener.Get (btnMXAttack).onClick = btnAttackHandler;
//		UUIEventListener.Get (btnLEAttack).onClick = btnAttackHandler;
//		UUIEventListener.Get (btnHMAttack).onClick = btnAttackHandler;

		
		UUIEventListener.Get (btnODUlt).onClick = BtnUtlHandler;
		UUIEventListener.Get (btnMXUlt).onClick = BtnUtlHandler;
		UUIEventListener.Get (btnLEUlt).onClick = BtnUtlHandler;
		UUIEventListener.Get (btnHMUlt).onClick = BtnUtlHandler;
		UUIEventListener.Get (btnRRUlt).onClick = BtnUtlHandler;


		//UUIEventListener.Get (btnStart).onClick = BtnStartClickHandler;

		UUIEventListener.Get(loadingImage).onDrag = LoadingDragHandler;
		UUIEventListener.Get(btnTimer).onClick = BtnTimerHandler;
		//MessageCenter.GetInstance.addEventListener (BaseEvent.CLICK, btn1ClickHandler);
		MainComponentManager main = MainComponentManager.SharedInstance;
		BattleManager battle = BattleManager.SharedInstance;
		
//		StartCoroutine (DelayToInvokeDo (2.0f));

		//播放第一段背景音乐
		AudioManager.SharedInstance.PlaySound("Bgmusic_01",1.0f);
	}

	void Update ()
	{

//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
//			SlashScreen ();
//			Debug.Log ("SlashScreen");
//		}



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
	void BtnUtlHandler (GameObject go)
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

	void Btn1ClickHandler (GameObject go)
	{
		
//		Debug.Log ("btn1ClickHandler");
//		AttackMessage message = new AttackMessage ();
		EventCenter.GetInstance.dispatchEvent (BattleEvent.ATTACK, 0);
	}

	void BtnStartClickHandler (GameObject go)
	{
		//Debug.Log("btnStartClickHandler");
		//BattleManager.SharedInstance.BattleStart ();
	}


	void LoadingDragHandler(GameObject go)
	{
		EventCenter.GetInstance.dispatchEvent (BattleEvent.SLASH, 0);
	}

	void BtnTimerHandler(GameObject go)
	{
		EventCenter.GetInstance.dispatchEvent (BattleEvent.CHANGE_LEVEL, 0);
	}

}
