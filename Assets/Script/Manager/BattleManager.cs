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
		new Character(1,"黑风",100,3,"Prefabs/hf"),
		new Character(2,"奥丁",120,5,"Prefabs/od"),
		new Character(3,"绿萼",110,4,"Prefabs/le"),
		new Character(4,"慕雪",110,4,"Prefabs/mx"),
		new Character(5,"寒梦",110,4,"Prefabs/hm")
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
		EventCenter.GetInstance.addEventListener (BattleEvent.ATTACK, BattleAttackHandler);
		//大招
		EventCenter.GetInstance.addEventListener (BattleEvent.ULT, BattleUltHandler);
	}


	/// <summary>
	/// 战斗开始
	/// </summary>
	public void BattleStart ()
	{

		FMOD_StudioSystem.instance.PlayOneShot("event:/battleBgStage1",Vector3.zero,0.4f);

		//Debug.Log ("Battle BattleStart");
		AddEnemy ();
		AddHero ();


	}

	/// <summary>
	/// 战斗结束
	/// </summary>
	private void BattleEnd ()
	{
		EventCenter.GetInstance.removeEventListener (BattleEvent.ATTACK, BattleAttackHandler);
		EventCenter.GetInstance.removeEventListener (BattleEvent.ULT, BattleUltHandler);
	}

	/// <summary>
	/// Adds the enemy.
	/// </summary>
	void AddEnemy ()
	{

		GameObject parent = StageManager.SharedInstance.NpcLayer;
		GameObject hfPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [0].Prefab);
		GameObject hf = StageManager.SharedInstance.AddToStage (parent, hfPrefab);

		BaseSoldier hfSoldier = hf.AddComponent<HFSoldier> (); 
		BattleAgent agent = new BattleAgent (hfSoldier, testDB [0]);

		agent.BaseSprite.SetStagePosition (-390, 0);


		enemyList.Add (agent);

 
	}

	IEnumerator AddOD()
	{
		yield return new WaitForSeconds(0.3f);
		//奥丁
		GameObject parent = StageManager.SharedInstance.HeroLayer;
		GameObject odPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [1].Prefab);	
		GameObject od = StageManager.SharedInstance.AddToStage (parent, odPrefab);
		
		BaseSoldier odSoldier = od.AddComponent<ODSoldier> ();
		BattleAgent agent = new BattleAgent (odSoldier, testDB [1]);
		
		agent.BaseSprite.SetStagePosition (260, -245);
		agent.BaseSprite.AddDownEffect();		
		
		soldierList.Add (agent);


		yield return new WaitForSeconds(0.3f);
		//绿萼
 
		GameObject lePrefab = ResourceManager.GetInstance.LoadPrefab (testDB [2].Prefab);
		GameObject le = StageManager.SharedInstance.AddToStage (parent, lePrefab);
		
		BaseSoldier leSoldier = le.AddComponent<LESoldier> ();
		BattleAgent agent2 = new BattleAgent (leSoldier, testDB [2]);
		
		agent2.BaseSprite.SetStagePosition (380, 120);
		agent2.BaseSprite.AddDownEffect();
		soldierList.Add (agent2);


		yield return new WaitForSeconds(0.3f);
		
		//慕雪
 
		GameObject mxPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [3].Prefab);
		GameObject mx = StageManager.SharedInstance.AddToStage (parent, mxPrefab);
		
		BaseSoldier mxSoldier = mx.AddComponent<MXSoldier> ();
		BattleAgent agent3 = new BattleAgent (mxSoldier, testDB [3]);
		
		agent3.BaseSprite.SetStagePosition (-208, -34);
		agent3.BaseSprite.AddDownEffect();
		soldierList.Add (agent3);


		yield return new WaitForSeconds(0.3f);
		//寒梦
 
		GameObject hmPrefab = ResourceManager.GetInstance.LoadPrefab (testDB [4].Prefab);
		GameObject hm = StageManager.SharedInstance.AddToStage (parent, hmPrefab);
		
		BaseSoldier hmSoldier = hm.AddComponent<HMSoldier> ();
		BattleAgent agent4 = new BattleAgent (hmSoldier, testDB [4]);
		
		agent4.BaseSprite.SetStagePosition (-99, -77);
		agent4.BaseSprite.AddDownEffect();
		soldierList.Add (agent4);


		enemyList [0].AddTarget (soldierList [0]);
		soldierList [0].AddTarget (enemyList [0]);
		soldierList [1].AddTarget (enemyList [0]);
		soldierList [2].AddTarget (enemyList [0]);
		soldierList [3].AddTarget (enemyList [0]);
	}


 


	/// <summary>
	/// Adds the hero.
	/// </summary>
	void AddHero ()
	{
		StartCoroutine("AddOD");
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

	void BattleAttackHandler (CEvent e)
	{
		int index = Convert.ToInt32 (e.data);

		if (index == 1) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Attack, null);
			AttackMessage message = new AttackMessage (soldierList [0], enemyList, 1);
			soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		}

		if (index == 2) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Attack, null);
			AttackMessage message = new AttackMessage (soldierList [1], enemyList, 1);
			soldierList [1].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		}

		if (index == 3) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Attack, null);
			AttackMessage message = new AttackMessage (soldierList [2], enemyList, 1);
			soldierList [2].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		}

		if (index == 4) {
			//soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Attack, null);
			AttackMessage message = new AttackMessage (soldierList [3], enemyList, 1);
			soldierList [3].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		}
		
		//黑风攻击
		if (index == 0) {
			//enemyList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Attack, null);

			AttackMessage message = new AttackMessage (enemyList [0], soldierList, 1);
			enemyList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
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
	public List<BattleAgent> GetEnemyList()
	{
		return enemyList;
	}
}
