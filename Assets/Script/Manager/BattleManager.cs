using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 战斗管理
/// 战斗过程相关都放这里
/// </summary>
using System.Collections;
using System.Threading;

public class BattleManager : MonoBehaviour
{
	private static BattleManager instance = null;
	
	public static BattleManager SharedInstance {
		get {
			if (instance == null) {
				instance = MainComponentManager.AddMainComponent<BattleManager> ();
			}
			return instance;
		}
	}



//	private static BattleManager instance;
//
//	public static BattleManager GetInstance {
//		get {
//			if (instance == null) {
//
//				instance = new BattleManager ();
//			}
//			return instance;
//		}
//	}

//	private List<Character> testDB = new List<Character> (){
//		new Character(1,"黑风",100,3,"Prefabs/hf",1),
//		new Character(2,"奥丁",120,5,"Prefabs/od",SkillData.MELEE),
//		new Character(3,"绿萼",110,4,"Prefabs/le",SkillData.MELEE),
//		new Character(4,"慕雪",110,4,"Prefabs/mx",1),
//		new Character(5,"寒梦",110,4,"Prefabs/hm",SkillData.MELEE),
//		new Character(6,"蓉蓉",110,4,"Prefabs/rr",SkillData.MELEE),
//		new Character(7,"阿莫",110,4,"Prefabs/am",SkillData.MELEE)
//	};

	/// <summary>
	/// 经过游戏时间
	/// </summary>
	private float timeElapsed = 0;

	/// <summary>
	/// 正在Loading
	/// </summary>
	private bool isLoading = false;


	/// <summary>
	/// 英雄
	/// </summary>
	private List<BattleAgent> soldierList = new List<BattleAgent> ();

	/// <summary>
	/// 敌人
	/// </summary>
	private List<BattleAgent> enemyList = new List<BattleAgent> ();
	private Dictionary<string,BattleAgent> agentDict = new Dictionary<string, BattleAgent> ();


	/// <summary>
	/// 敌人站位
	/// </summary>
	private Vector2[] enemyPos = new Vector2[] {
		new Vector2 (9, 5),
		new Vector2 (5, 8),
		new Vector2 (7, 7)
//		new Vector2 (9, 9),
//		new Vector2 (7, 7)
	};

	/// <summary>
	/// 英雄站位
	/// </summary>
	private Vector2[] soldierPos = new Vector2[] {
		new Vector2 (10, 3),
		new Vector2 (3, 8),
		new Vector2 (3, 3)
//		new Vector2 (12, 6),
//		new Vector2 (13, 8)
	};
	
	void Awake ()
	{
//		Debug.Log ("BattleManager");

		//开始攻击
//		EventCenter.GetInstance.addEventListener (BattleEvent.ATTACK, BattleAttackHandler);
		//大招
		EventCenter.GetInstance.addEventListener (BattleEvent.ULT, BattleUltHandler);
		EventCenter.GetInstance.addEventListener (BattleEvent.SLASH, SlashHandler);
		EventCenter.GetInstance.addEventListener (BattleEvent.CHANGE_LEVEL, ChangeLevelHandler);

	}


	/// <summary>
	/// 战斗开始
	/// </summary>
	public void BattleStart ()
	{

		FMOD_StudioSystem.instance.PlayOneShot ("event:/battleBgStage1", Vector3.zero, 0.4f);

		//Debug.Log ("Battle BattleStart");
		AddEnemy ();
		AddHero ();


	}

	/// <summary>
	/// 战斗结束
	/// </summary>
	private void BattleEnd ()
	{
//		EventCenter.GetInstance.removeEventListener (BattleEvent.ATTACK, BattleAttackHandler);
		EventCenter.GetInstance.removeEventListener (BattleEvent.ULT, BattleUltHandler);
	}

	/// <summary>
	/// 开始游戏计时
	/// </summary>
	void StartTimer ()
	{
		InvokeRepeating ("timerHandler", 1.0f, 1.0f);
		//gameTime = TimerManager.SharedInstance.CreateTimer (1.0f, timerHandler);

	}

	void timerHandler ()
	{
		GameObject timeText = GameObject.Find ("time");
		Text txt = timeText.GetComponent<Text> ();

		timeElapsed = timeElapsed + 1;

		txt.text = timeElapsed + "";
	}

	/// <summary>
	/// Adds the enemy.
	/// </summary>
	void AddEnemy ()
	{

		GameObject parent = StageManager.SharedInstance.NpcLayer;

//		Vector2[] pos = new Vector2[] {
//			new Vector2 (4, 4),
//			new Vector2 (5, 9),
//			new Vector2 (9, 3),
//			new Vector2 (8, 9)
//		};

		for (int i = 0; i < enemyPos.Length - 1; i++) {

			GameObject hfPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [11].Prefab);
			GameObject hf = StageManager.SharedInstance.AddToStage (parent, hfPrefab);

			hf.name = "hf" + i;

			BaseSoldier hfSoldier = hf.AddComponent<HFSoldier> (); 
			BattleAgent agent = new BattleAgent (hfSoldier, TestData.charDB [11]);
			agent.BaseSprite.FaceTo = FaceTo.Right;
			
			//agent.BaseSprite.SetLocalPosition (pos [i].x, pos [i].y);
			agent.BaseSprite.SetMapPosition (enemyPos [i].x, enemyPos [i].y);
			enemyList.Add (agent);

			agentDict.Add (hf.name, agent);

		}



		//boss
		GameObject prefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [17].Prefab);
		GameObject am = StageManager.SharedInstance.AddToStage (parent, prefab);
		am.name = "am";
		BaseSoldier amSoldier = am.AddComponent<AMSoldier> (); 

		Character c = TestData.charDB [17];
		c.Speed = 3.0f;
		BattleAgent amAgent = new BattleAgent (amSoldier, c);

		amAgent.Character.HitPoint = new Vector3 (0, 100, 100);

		//agent.BaseSprite.SetLocalPosition (pos [i].x, pos [i].y);
		amAgent.BaseSprite.SetMapPosition (enemyPos [2].x, enemyPos [2].y);
		enemyList.Add (amAgent);

		agentDict.Add (am.name, amAgent);
	}

	IEnumerator AddOD ()
	{
		yield return new WaitForSeconds (0.2f);
		//奥丁
		GameObject parent = StageManager.SharedInstance.HeroLayer;
		GameObject odPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [12].Prefab);	
		GameObject od = StageManager.SharedInstance.AddToStage (parent, odPrefab);
		od.name = "od";
		BaseSoldier odSoldier = od.AddComponent<ODSoldier> ();
		BattleAgent odAgent = new BattleAgent (odSoldier, TestData.charDB [12]);
		
		odAgent.BaseSprite.SetMapPosition (soldierPos [0].x, soldierPos [0].y);
		odAgent.BaseSprite.AddDownEffect ();		
		
		soldierList.Add (odAgent);
		agentDict.Add (od.name, odAgent);
		
		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);



		yield return new WaitForSeconds (0.2f);

		//绿萼
 
//		GameObject lePrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [13].Prefab);
//		GameObject le = StageManager.SharedInstance.AddToStage (parent, lePrefab);
//		le.name = "le";
//		BaseSoldier leSoldier = le.AddComponent<LESoldier> ();
//		BattleAgent leAgent = new BattleAgent (leSoldier, TestData.charDB [13]);
//		leAgent.BaseSprite.SetMapPosition (soldierPos [1].x, soldierPos [1].y);
//
//		leAgent.BaseSprite.AddDownEffect ();
//		soldierList.Add (leAgent);
//		agentDict.Add (le.name, leAgent);
//		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);
//		yield return new WaitForSeconds (0.2f);
		
		//慕雪
 
		GameObject mxPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [14].Prefab);
		GameObject mx = StageManager.SharedInstance.AddToStage (parent, mxPrefab);
		mx.name = "mx";
		BaseSoldier mxSoldier = mx.AddComponent<MXSoldier> ();
		BattleAgent mxAgent = new BattleAgent (mxSoldier, TestData.charDB [14]);
		

		mxAgent.BaseSprite.SetMapPosition (soldierPos [1].x, soldierPos [1].y);
		mxAgent.BaseSprite.AddDownEffect ();
		soldierList.Add (mxAgent);
		agentDict.Add (mx.name, mxAgent);
		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);
		yield return new WaitForSeconds (0.2f);
		//寒梦
 
		GameObject hmPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [15].Prefab);
		GameObject hm = StageManager.SharedInstance.AddToStage (parent, hmPrefab);
		hm.name = "hm";
		BaseSoldier hmSoldier = hm.AddComponent<HMSoldier> ();
		BattleAgent hmAgent = new BattleAgent (hmSoldier, TestData.charDB [15]);
		
		hmAgent.BaseSprite.SetMapPosition (soldierPos [2].x, soldierPos [2].y);
		hmAgent.BaseSprite.AddDownEffect ();
		soldierList.Add (hmAgent);
		agentDict.Add (hm.name, hmAgent);
		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);
		yield return new WaitForSeconds (0.3f);
		//蓉蓉
		
//		GameObject rrPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [16].Prefab);
//		GameObject rr = StageManager.SharedInstance.AddToStage (parent, rrPrefab);
//		rr.name = "rr";
//		BaseSoldier rrSoldier = rr.AddComponent<RRSoldier> ();
//		BattleAgent rrAgent = new BattleAgent (rrSoldier, TestData.charDB [16]);
//		
//		rrAgent.BaseSprite.SetMapPosition (soldierPos [4].x, soldierPos [4].y);
//		rrAgent.BaseSprite.AddDownEffect ();
//		soldierList.Add (rrAgent);
//		agentDict.Add (rr.name, rrAgent);
//		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);




//		FindTargetForHero (odAgent);
	
//		soldierList [0].AddTarget (enemyList [0]);
//		soldierList [1].AddTarget (enemyList [0]);
//		soldierList [2].AddTarget (enemyList [0]);
//		soldierList [3].AddTarget (enemyList [0]);
//		soldierList [4].AddTarget (enemyList [0]);


		//enemyList [0].AddTarget (rrAgent);
		//enemyList [0].AddTarget (odAgent);
		enemyList [0].AddTarget (odAgent);
		enemyList [1].AddTarget (mxAgent);
		enemyList [2].AddTarget (hmAgent);

		odAgent.AddTarget (enemyList [0]);
		mxAgent.AddTarget (enemyList [1]);
		hmAgent.AddTarget (enemyList [2]);


		//leAgent.AddTarget (enemyList [1]);

		//rrAgent.AddTarget (enemyList [0]);

//		hmAgent.IsReady = true;
//		enemyList [4].IsReady = true;

// 		enemyList[4].IsReady = true;
// 		mxAgent.IsReady = true;
 

// 		leAgent.IsReady = true;
// 		enemyList[1].IsReady = true;

		
//		 		rrAgent.IsReady = true;
//		 		enemyList[0].IsReady = true;


//		for (int i = 0; i < soldierList.Count; i++) {
//			soldierList [i].IsReady = true;
//		}
//
//		for (int i = 0; i < enemyList.Count; i++) {
//			enemyList [i].IsReady = true;
//		}
	}
 
	void AddHero2 ()
	{
		GameObject parent = StageManager.SharedInstance.HeroLayer;
		GameObject odPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [12].Prefab);	
		GameObject od = StageManager.SharedInstance.AddToStage (parent, odPrefab);
		
		BaseSoldier odSoldier = od.AddComponent<ODSoldier> ();
		BattleAgent agent = new BattleAgent (odSoldier, TestData.charDB [12]);


		agent.BaseSprite.SetMapPosition (10, 15);
		//agent.BaseSprite.SetLocalPosition (260, -245);
		agent.BaseSprite.AddDownEffect ();		
		
		soldierList.Add (agent);

		enemyList [0].AddTarget (soldierList [0]);
		soldierList [0].AddTarget (enemyList [0]);
	}

	/// <summary>
	/// Adds the hero.
	/// </summary>
	void AddHero ()
	{
		StartCoroutine (AddOD ());
	}

	void BattleUltHandler (CEvent e)
	{
		int index = Convert.ToInt32 (e.data);

		//奥丁大招 2000x 的技能ID
		if (index == 11) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (agentDict ["od"], enemyList, 20001);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);
		}
		if (index == 21) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (agentDict ["le"], enemyList, 20002);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);
		}
		if (index == 31) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (agentDict ["mx"], enemyList, 20003);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);
		}
		if (index == 41) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (agentDict ["hm"], enemyList, 20004);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);
		}

		if (index == 51) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (agentDict ["rr"], enemyList, 20005);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);

		}
	}

	void BattleStartHandler (CEvent e)
	{
		BattleStart ();
		//Debug.Log ("battleStartHandler");

	}


	/// <summary>
	/// 获取敌人
	/// </summary>
	/// <returns>The enemy list.</returns>
	public List<BattleAgent> GetEnemyList ()
	{
		return enemyList;
	}

	public List<BattleAgent> GetHeroList ()
	{
		return soldierList;
	}

	/// <summary>
	/// 获取所有物体位置s
	/// </summary>
	/// <returns>The agent list.</returns>
	public List<BattleAgent> GetAgentList ()
	{
		List<BattleAgent> list = new List<BattleAgent> ();
		list.AddRange (enemyList);
		list.AddRange (soldierList);

		return list;
	}

	public void FindMyEnemy (BattleAgent agent)
	{
		if (agent.BaseSoldier.GetType () == typeof(EnemySoldier)) {
			FindTargetForEnemy (agent);
		}

		if (agent.BaseSoldier.GetType () == typeof(HeroSoldier)) {
			FindTargetForHero (agent);
		}
	}

	/// <summary>
	/// 查找最近的目标
	/// </summary>
	private void FindTargetForHero (BattleAgent hero)
	{
		
		List<BattleAgent> list = BattleManager.SharedInstance.GetEnemyList ();
		
		List<float> disList = new List<float> ();
		
		for (int i = 0; i < list.Count; i++) {
			
			float dis = Vector2.Distance (hero.MapPos, list [i].MapPos);
			
			disList.Add (dis);
		}
		
		float min = Mathf.Min (disList.ToArray ());
		
		for (int j = 0; j < disList.Count; j++) {
			
			if (min == disList [j]) {
				hero.AddTarget (list [j]);
			}
			
		}
	}


	/// <summary>
	/// 查找最近的目标
	/// </summary>
	private void FindTargetForEnemy (BattleAgent hero)
	{
		
		List<BattleAgent> list = BattleManager.SharedInstance.GetEnemyList ();
		
		List<float> disList = new List<float> ();
		
		for (int i = 0; i < list.Count; i++) {
			
			float dis = Vector2.Distance (hero.MapPos, list [i].MapPos);
			
			disList.Add (dis);
		}
		
		float min = Mathf.Min (disList.ToArray ());
		
		for (int j = 0; j < disList.Count; j++) {
			
			if (min == disList [j]) {
				hero.AddTarget (list [j]);
			}
			
		}
	}


	/// <summary>
	/// 查找浮空目标
	/// </summary>
	/// <returns>The float target.</returns>
	public List<BattleAgent> GetFloatTarget ()
	{
		List<BattleAgent> floatList = new List<BattleAgent> ();
		for (int i = 0; i < enemyList.Count; i++) {
			if (enemyList [i].BaseSoldier.StateMachine.CurrentState.StateId == StateId.Float) {
				floatList.Add (enemyList [i]);
			}
		}

		return floatList;
	}
	
	void SlashHandler (CEvent e)
	{
		//EventCenter.GetInstance.removeEventListener (BattleEvent.SLASH, SlashHandler);
		//Debug.Log("SlashHandler");
		if (isLoading) {
			return;
		}
		AudioManager.SharedInstance.PlayOneShot ("fight", 2.0f);
		StartCoroutine (SlashScreen ());
	}

	void ChangeLevelHandler (CEvent e)
	{
		StartCoroutine (ChangeMap ());
	}
	






	/// <summary>
	/// 滑动屏幕
	/// </summary>
	IEnumerator SlashScreen ()
	{
		AddEnemy ();

		AudioManager.SharedInstance.PlaySound ("Bgmusic_02", 1.0f, true);

		Hashtable args = new Hashtable ();
		args.Add ("time", 1.0f);
		args.Add ("x", Camera.main.gameObject.transform.localPosition.x + 1);
		args.Add ("y", Camera.main.gameObject.transform.localPosition.y + 1);
		
		iTween.PunchPosition (StageManager.SharedInstance.LoadingLayer, args);
		
		GameObject loadingImage = GameObject.Find ("Loading");

		isLoading = true;
		GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [20].Prefab);
		GameObject parent = StageManager.SharedInstance.SlashLayer; 
		GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
		BaseEffect baseEffect = bullet.AddComponent<BaseEffect> (); 
		bullet.transform.position = loadingImage.transform.position;
		yield return new WaitForSeconds (0.5f);
		
		Destroy (loadingImage);
		Destroy (bullet);
		
		GameObject white = GameObject.Find ("whiteMask");
		
		Hashtable args2 = new Hashtable ();
		args2.Add ("time", 2.0f);
		args2.Add ("alpha", 0);
		args2.Add ("oncomplete", "MaskFadeComplete");
		//args.Add ("oncompletetarget", this.gameObject);
		args2.Add ("ignoretimescale", true);
		//Image img = blackMask.GetComponent<Image>();
		iTween.FadeTo (white, args2);
		
		yield return new WaitForSeconds (1.0f);
		
		Destroy (white);



		AudioManager.SharedInstance.StopSound ("Bgmusic_01");

		Proceed ();

	}


	/// <summary>
	/// 进程处理
	/// </summary>
	void Proceed ()
	{
	
		StartTimer ();

		StartCoroutine (Step1 ());
	

	}


	/// <summary>
	/// 删除敌人
	/// </summary>
	public void RemoveEnemy (BattleAgent ba)
	{
		if (enemyList.Contains (ba)) {

			this.enemyList.Remove (ba);
			agentDict.Remove (ba.BaseSprite.gameObject.name);

			for (int i = 0; i < soldierList.Count; i++) {

				soldierList [i].RemoveTarget (ba);
			}
		}

		Destroy (ba.BaseSprite.gameObject);
	}

	/// <summary>
	/// 移除英雄
	/// </summary>
	/// <param name="ba">Ba.</param>
	public void RemoveSoldier (BattleAgent ba)
	{
		if (soldierList.Contains (ba)) {
			
			this.soldierList.Remove (ba);
			agentDict.Remove (ba.BaseSprite.gameObject.name);
			for (int i = 0; i < enemyList.Count; i++) {

				enemyList [i].RemoveTarget (ba);
			}
		}

		Destroy (ba.BaseSprite.gameObject);
	}


	/// <summary>
	/// 落阵动画
	/// 对白
	/// </summary>
	IEnumerator Step1 ()
	{
		GameObject parent = StageManager.SharedInstance.EffectLayer;

		yield return new WaitForSeconds (0.2f);

		for (int i = 0; i < soldierPos.Length; i++) {

			GameObject bulletPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [21].Prefab);

			GameObject bullet = StageManager.SharedInstance.AddToStage (parent, bulletPrefab);
			BaseBullet pos = bullet.AddComponent<BaseBullet> (); 
			pos.transform.position = MapUtil.GetInstance.MapToWorld (soldierPos [i].x, soldierPos [i].y);

		}

		yield return new WaitForSeconds (0.2f);

		//英雄落位
		AddHero ();

		//感叹号
		for (int i = 0; i < enemyList.Count -1; i++) {

			enemyList [i].dispatchEvent (SoldierEvent.SURPRISE, null);

		}


		yield return new WaitForSeconds (0.5f);

		//奥丁说话
		GameObject popoPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [24].Prefab);
		GameObject popo = StageManager.SharedInstance.AddToStage (parent, popoPrefab);
		Popo txt = popo.AddComponent<Popo> (); 
		txt.transform.position = MapUtil.GetInstance.MapToWorld (soldierPos [0].x, soldierPos [0].y);
		txt.SetText ("大家就位，做好战斗准备！");

		yield return new WaitForSeconds (2.0f);
		Destroy (popo);


		//朱雀阵型
		GameObject zhuquePrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [22].Prefab);
		
		GameObject zhuque = StageManager.SharedInstance.AddToStage (parent, zhuquePrefab);
		BaseEffect zhuqueEffect = zhuque.AddComponent<BaseEffect> (); 
		zhuqueEffect.transform.position = MapUtil.GetInstance.MapToWorld (8, 6);


		Hashtable args1 = new Hashtable ();
		args1.Add ("time", 1.5f);
		args1.Add ("alpha", 0f);
		iTween.FadeTo (zhuque, args1);
	
		yield return new WaitForSeconds (2.0f);
		Destroy (zhuque);

		//阿莫说话
		GameObject popo2Prefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [24].Prefab);
		GameObject popo2 = StageManager.SharedInstance.AddToStage (parent, popo2Prefab);
		Popo txt2 = popo2.AddComponent<Popo> (); 
		txt2.transform.position = MapUtil.GetInstance.MapToWorld (enemyPos [2].x, enemyPos [2].y);
		txt2.SetText ("小的们，野狗阵型走起！");

		yield return new WaitForSeconds (2.0f);
		
		Destroy (popo2);

		//白虎阵型
		GameObject baihuPrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [23].Prefab);
		
		GameObject baihu = StageManager.SharedInstance.AddToStage (parent, baihuPrefab);
		BaseEffect baihuEffect = baihu.AddComponent<BaseEffect> (); 
		baihuEffect.transform.position = MapUtil.GetInstance.MapToWorld (8, 6);


		Hashtable args2 = new Hashtable ();
		args2.Add ("time", 1.5f);
		args2.Add ("alpha", 0f);
		iTween.FadeTo (baihu, args2);


		yield return new WaitForSeconds (2.0f);
		Destroy (baihu);


//		enemyList [3].IsReady = true;
//		soldierList [2].IsReady = true;

		for (int i = 0; i < soldierList.Count; i++) {
			soldierList [i].IsReady = true;
		}
		
		for (int i = 0; i < enemyList.Count; i++) {
			enemyList [i].IsReady = true;
		}

	}


	/// <summary>
	/// 切换地图
	/// </summary>
	/// <returns>The map.</returns>
	IEnumerator ChangeMap ()
	{

		//隐藏大招按钮
		GameObject.Find ("btnOD").SetActive (false);
//		GameObject.Find ("btnLE").SetActive (false);
//		GameObject.Find ("btnRR").SetActive (false);


		for (int i = 0; i < enemyList.Count; i++) {
			enemyList [i].IsReady = false;
			enemyList [i].BaseSoldier.OnIdle ();
		}

		for (int i = 0; i < soldierList.Count; i++) {
			soldierList [i].IsReady = false;
			soldierList [i].BaseSoldier.OnIdle ();
		}
		
//		soldierList [0].IsReady = false;
//		soldierList [1].IsReady = false;
//		soldierList [4].IsReady = false;
		
		//阿莫说话
		GameObject parent = StageManager.SharedInstance.EffectLayer;
		GameObject popo2Prefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [24].Prefab);
		GameObject popo2 = StageManager.SharedInstance.AddToStage (parent, popo2Prefab);
		Popo txt2 = popo2.AddComponent<Popo> (); 
		txt2.transform.position = MapUtil.GetInstance.MapToWorld (enemyList [2].MapPos.x, enemyList [2].MapPos.y);
		txt2.SetText ("小的们顶住！我妈妈喊我回家吃饭！");
		
		yield return new WaitForSeconds (2.0f);
		
		Destroy (popo2);

		//am上天
		BattleAgent amAgent = enemyList [2];
		amAgent.BaseSprite.AddBlackDownEffect ();
		amAgent.BaseSprite.gameObject.SetActive (false);
		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);
		yield return new WaitForSeconds (1.0f);

		//mx  hm一起上天
		BattleAgent hmAgent = soldierList [2];
		hmAgent.BaseSprite.AddDownEffect ();
		hmAgent.BaseSprite.gameObject.SetActive (false);



		BattleAgent mxAgent = soldierList [1];
		mxAgent.BaseSprite.AddDownEffect ();
		mxAgent.BaseSprite.gameObject.SetActive (false);

		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);

		//yield return new WaitForSeconds (0.5f);

		//AudioManager.SharedInstance.PlaySound ("Bgmusic_02", 1.0f);




		//变换背景
		GameObject slashLayer = StageManager.SharedInstance.SlashLayer;
		GameObject whitePrefab = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [26].Prefab);
		GameObject white = StageManager.SharedInstance.AddToStage (slashLayer, whitePrefab);
		Image img = white.GetComponent<Image> ();
		Color c = new Color (img.color.r, img.color.g, img.color.b, 0);
		img.color = c;

		Hashtable args2 = new Hashtable ();
		args2.Add ("time", 0.5f);
		args2.Add ("alpha", 1.0f);
		iTween.FadeTo (white, args2);

		yield return new WaitForSeconds (0.5f);

		mxAgent.BaseSprite.gameObject.SetActive (true);
		hmAgent.BaseSprite.gameObject.SetActive (true);
		amAgent.BaseSprite.gameObject.SetActive (true);


		mxAgent.ClearTarget ();
		mxAgent.AddTarget (amAgent);
		hmAgent.ClearTarget ();
		hmAgent.AddTarget (amAgent);
		amAgent.ClearTarget ();
		amAgent.AddTarget (hmAgent);


		//重新设定新位置
		mxAgent.BaseSprite.SetMapPosition (3, 2);
		hmAgent.BaseSprite.SetMapPosition (10, 8);
		amAgent.BaseSprite.SetMapPosition (6, 6);

		
		mxAgent.BaseSprite.gameObject.SetActive (false);
		hmAgent.BaseSprite.gameObject.SetActive (false);
		amAgent.BaseSprite.gameObject.SetActive (false);


		//移除不要的角色

		List<BattleAgent> list1 = new List<BattleAgent> ();
		for (int i = 0; i < enemyList.Count; i++) {

			if (enemyList [i] == amAgent) {
				continue;
			}
			list1.Add (enemyList [i]);
		}
	
		for (int i = 0; i < list1.Count; i++) {
			RemoveEnemy (list1 [i]);
		}


		List<BattleAgent> list2 = new List<BattleAgent> ();
		for (int i = 0; i < soldierList.Count; i++) {
			if (soldierList [i] == hmAgent) {
				continue;
			}

			if (soldierList [i] == mxAgent) {
				continue;
			}
			list2.Add (soldierList [i]);
		}

		for (int i = 0; i < list2.Count; i++) {
			RemoveSoldier (list2 [i]);
		}
		

		yield return new WaitForSeconds (1f);

		GameObject bg2 = ResourceManager.GetInstance.LoadPrefab (TestData.charDB [28].Prefab);
		StageManager.SharedInstance.ChangeBg (bg2);

		Hashtable args3 = new Hashtable ();
		args3.Add ("time", 1f);
		args3.Add ("alpha", 0);
		iTween.FadeTo (white, args3);

		yield return new WaitForSeconds (0.5f);
		Destroy (white);



		//重新落位
		mxAgent.BaseSprite.AddDownEffect ();
		mxAgent.BaseSprite.gameObject.SetActive (true);
		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);
		yield return new WaitForSeconds (0.1f);

		hmAgent.BaseSprite.AddDownEffect ();
		hmAgent.BaseSprite.gameObject.SetActive (true);
		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);
		yield return new WaitForSeconds (0.1f);

		amAgent.BaseSprite.AddBlackDownEffect ();
		amAgent.BaseSprite.gameObject.SetActive (true);
		AudioManager.SharedInstance.PlayOneShot ("fly", 1.0f);


		mxAgent.IsReady = true;
		hmAgent.IsReady = true;
		amAgent.IsReady = true;



	}

	
}
