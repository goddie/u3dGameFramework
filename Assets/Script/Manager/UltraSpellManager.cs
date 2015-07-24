
using System;

/// <summary>
/// 必杀技
/// 大招效果控制
/// 角色高亮、屏幕遮罩、时间变慢
/// </summary>
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class UltraSpellManager : MonoBehaviour
{
	private static UltraSpellManager instance = null;
	
	public static UltraSpellManager SharedInstance {
		get {
			if (instance == null) {

				instance = MainComponentManager.AddMainComponent<UltraSpellManager> ();
			}
			return instance;
		}
	}

	private GameObject blackMask;
	private BaseEffect baseEffect;
	private List<Character> testDB = new List<Character> (){
		new Character(203,"大招特效",100,3,"Prefabs/worldUlt",0),
		new Character(203,"大招遮罩",100,3,"Prefabs/mask",0)
	};

	void Start ()
	{
		addMask ();

		EventCenter.GetInstance.addEventListener (SoldierEvent.ULT_LOAD, BattleUltLoadHandler);
		
	}


	/// <summary>
	/// 打开遮罩
	/// </summary>
	void addMask ()
	{

		if (blackMask == null) {
			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [1].Prefab);
			GameObject parent = StageManager.SharedInstance.MaskLayer; 
			blackMask = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);

		}

		blackMask.SetActive (false);
		Image img = blackMask.GetComponent<Image> ();
		float aa = 0.8f;


		Color c = new Color (img.color.r, img.color.g, img.color.b, aa);

		img.color = c;

	}

	/// <summary>
	/// 大招引导特效开始
	/// </summary>
	/// <param name="c">C.</param>
	void BattleUltLoadHandler (CEvent c)
	{
		AttackMessage message = (AttackMessage)c.data;	
		StartCoroutine ("PlayUltEffect", message);
	}

	/// <summary>
	/// 震动背景
	/// </summary>
	void ShakeBg ()
	{

		Hashtable args = new Hashtable ();
		args.Add ("time", 1.0f);
		args.Add ("x", Camera.main.gameObject.transform.localPosition.x + 1);
		args.Add ("y", Camera.main.gameObject.transform.localPosition.y + 1);
		
		iTween.PunchPosition (StageManager.SharedInstance.BgLayer, args);
	}

	/// <summary>
	/// 遮罩淡入淡出
	/// </summary>
	void MaskFade ()
	{
		blackMask.SetActive (true);
		Hashtable args = new Hashtable ();
		args.Add ("time", 1f);
		args.Add ("alpha", 0);

		//Image img = blackMask.GetComponent<Image>();

		iTween.FadeTo (blackMask, args);	
	}

	IEnumerator PlayUltEffect (AttackMessage message)
	{
		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [0].Prefab);
		GameObject parent = StageManager.SharedInstance.EffectLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
		baseEffect = bullet.AddComponent<BaseEffect> ();
		baseEffect.transform.position = message.Sender.GameObject.transform.position;

		BattleAgent battleAgent = message.Sender;
		Vector3 pos = MapUtil.RelativeMovePosition (battleAgent.BaseSprite.HitPoint, battleAgent.GameObject.transform);
		baseEffect.transform.position = new Vector3 (pos.x, pos.y, battleAgent.GameObject.transform.position.z);


		baseEffect.PlayOnAgent (message);

		MaskFade ();
		ShakeBg ();

		AudioManager.SharedInstance.FMODEvent ("world_ult3", 2.0f);

		yield return new WaitForSeconds (0.3f);

		addMask ();

		message.Sender.dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
	}



	/// <summary>
	/// 开始大招特效
	/// </summary>
	/// <param name="battleAgent">Battle agent.</param>
	/// <param name="attackMessage">Attack message.</param>
	public void StartUltraSpell (BattleAgent battleAgent, AttackMessage attackMessage)
	{

	}




}
