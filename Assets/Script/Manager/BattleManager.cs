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

	private List<Character> testDB = new List<Character> (){
		new Character(1,"黑风",100,3,"Prefabs/hf",2),
		new Character(2,"奥丁",120,5,"Prefabs/od",3),
		new Character(3,"绿萼",110,4,"Prefabs/le",3),
		new Character(4,"慕雪",110,4,"Prefabs/mx",2),
		new Character(5,"寒梦",110,4,"Prefabs/hm",3),
		new Character(6,"蓉蓉",110,4,"Prefabs/rr",3),
		new Character(7,"阿莫",110,4,"Prefabs/am",2)
	};


	/// <summary>
	/// 英雄
	/// </summary>
	private List<BattleAgent> soldierList = new List<BattleAgent> ();

	/// <summary>
	/// 敌人
	/// </summary>
	private List<BattleAgent> enemyList = new List<BattleAgent> ();
	
	private BattleManager ()
	{
		//Debug.Log ("BattleManager");

		//开始攻击
//		EventCenter.GetInstance.addEventListener (BattleEvent.ATTACK, BattleAttackHandler);
		//大招
		EventCenter.GetInstance.addEventListener (BattleEvent.ULT, BattleUltHandler);
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
	/// Adds the enemy.
	/// </summary>
	void AddEnemy ()
	{

		GameObject parent = StageManager.SharedInstance.NpcLayer;

		Vector2[] pos = new Vector2[] {
			new Vector2 (4, 4),
			new Vector2 (5, 9),
			new Vector2 (9, 3),
			new Vector2 (8, 9)
		};

		for (int i = 0; i < pos.Length; i++) {
			GameObject hfPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [0].Prefab);
			GameObject hf = StageManager.SharedInstance.AddToStage (parent, hfPrefab);

			hf.name = "hf" + i;

			BaseSoldier hfSoldier = hf.AddComponent<HFSoldier> (); 
			BattleAgent agent = new BattleAgent (hfSoldier, testDB [0]);
			agent.BaseSprite.FaceTo = 1;
			
			//agent.BaseSprite.SetLocalPosition (pos [i].x, pos [i].y);
			agent.BaseSprite.SetMapPosition (pos [i].x, pos [i].y);
			enemyList.Add (agent);
		}

		//boss
		GameObject prefab = ResourceManager.GetInstance.LoadPrefab (testDB [6].Prefab);
		GameObject am = StageManager.SharedInstance.AddToStage (parent, prefab);
		
		BaseSoldier amSoldier = am.AddComponent<AMSoldier> (); 
		BattleAgent amAgent = new BattleAgent (amSoldier, testDB [6]);
		amAgent.BaseSprite.FaceTo = 1;
		
		//agent.BaseSprite.SetLocalPosition (pos [i].x, pos [i].y);
		amAgent.BaseSprite.SetMapPosition (7, 7);
		enemyList.Add (amAgent);


	}

	IEnumerator AddOD ()
	{
		yield return new WaitForSeconds (0.2f);
		//奥丁
		GameObject parent = StageManager.SharedInstance.HeroLayer;
		GameObject odPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [1].Prefab);	
		GameObject od = StageManager.SharedInstance.AddToStage (parent, odPrefab);
		
		BaseSoldier odSoldier = od.AddComponent<ODSoldier> ();
		BattleAgent odAgent = new BattleAgent (odSoldier, testDB [1]);
		
		odAgent.BaseSprite.SetMapPosition (13, 3);
		odAgent.BaseSprite.AddDownEffect ();		
		
		soldierList.Add (odAgent);


		yield return new WaitForSeconds (0.2f);

		//绿萼
 
		GameObject lePrefab = ResourceManager.GetInstance.LoadPrefab (testDB [2].Prefab);
		GameObject le = StageManager.SharedInstance.AddToStage (parent, lePrefab);
		
		BaseSoldier leSoldier = le.AddComponent<LESoldier> ();
		BattleAgent leAgent = new BattleAgent (leSoldier, testDB [2]);
		leAgent.BaseSprite.SetMapPosition (1, 8);

		leAgent.BaseSprite.AddDownEffect ();
		soldierList.Add (leAgent);


		yield return new WaitForSeconds (0.2f);
		
		//慕雪
 
		GameObject mxPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [3].Prefab);
		GameObject mx = StageManager.SharedInstance.AddToStage (parent, mxPrefab);
		
		BaseSoldier mxSoldier = mx.AddComponent<MXSoldier> ();
		BattleAgent mxAgent = new BattleAgent (mxSoldier, testDB [3]);
		

		mxAgent.BaseSprite.SetMapPosition (1, 3);
		mxAgent.BaseSprite.AddDownEffect ();
		soldierList.Add (mxAgent);


		yield return new WaitForSeconds (0.2f);
		//寒梦
 
		GameObject hmPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [4].Prefab);
		GameObject hm = StageManager.SharedInstance.AddToStage (parent, hmPrefab);
		
		BaseSoldier hmSoldier = hm.AddComponent<HMSoldier> ();
		BattleAgent hmAgent = new BattleAgent (hmSoldier, testDB [4]);
		
		hmAgent.BaseSprite.SetMapPosition (13, 7);
		hmAgent.BaseSprite.AddDownEffect ();
		soldierList.Add (hmAgent);

		yield return new WaitForSeconds (0.3f);
		//蓉蓉
		
		GameObject rrPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [5].Prefab);
		GameObject rr = StageManager.SharedInstance.AddToStage (parent, rrPrefab);
		
		BaseSoldier rrSoldier = rr.AddComponent<RRSoldier> ();
		BattleAgent rrAgent = new BattleAgent (rrSoldier, testDB [5]);
		
		rrAgent.BaseSprite.SetMapPosition (15, 9);
		rrAgent.BaseSprite.AddDownEffect ();
		soldierList.Add (rrAgent);
		

//		FindTargetForHero (odAgent);
	
//		soldierList [0].AddTarget (enemyList [0]);
//		soldierList [1].AddTarget (enemyList [0]);
//		soldierList [2].AddTarget (enemyList [0]);
//		soldierList [3].AddTarget (enemyList [0]);
//		soldierList [4].AddTarget (enemyList [0]);


		enemyList [0].AddTarget (mxAgent);
		enemyList [1].AddTarget (leAgent);
		enemyList [2].AddTarget (odAgent);
		enemyList [3].AddTarget (hmAgent);
		enemyList [4].AddTarget (rrAgent);

		odAgent.AddTarget (enemyList [2]);
		leAgent.AddTarget (enemyList [1]);
		hmAgent.AddTarget (enemyList [3]);

		for (int i = 0; i < soldierList.Count; i++) {
			soldierList [i].IsReady = true;
		}

		for (int i = 0; i < enemyList.Count; i++) {
			enemyList [i].IsReady = true;
		}
	}

 
	void AddHero2 ()
	{
		GameObject parent = StageManager.SharedInstance.HeroLayer;
		GameObject odPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [1].Prefab);	
		GameObject od = StageManager.SharedInstance.AddToStage (parent, odPrefab);
		
		BaseSoldier odSoldier = od.AddComponent<ODSoldier> ();
		BattleAgent agent = new BattleAgent (odSoldier, testDB [1]);


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
			AttackMessage message = new AttackMessage (soldierList [0], enemyList, 20001);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);
		}
		if (index == 21) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (soldierList [1], enemyList, 20002);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);
		}
		if (index == 31) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (soldierList [2], enemyList, 20003);
			EventCenter.GetInstance.dispatchEvent (SoldierEvent.ULT_LOAD, message);
		}
		if (index == 41) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			//soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
			AttackMessage message = new AttackMessage (soldierList [3], enemyList, 20004);
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
	
}
