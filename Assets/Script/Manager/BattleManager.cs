using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 战斗管理
/// 战斗过程相关都放这里
/// </summary>
public class BattleManager
{
	private static BattleManager instance;

	public static BattleManager GetInstance {
		get {
			if (instance == null) {

				instance = new BattleManager ();
			}
			return instance;
		}
	}

	private List<Character> testDB= new List<Character>(){
		new Character(1,"黑风",100,3,"Prefabs/hf"),new Character(2,"奥丁",120,5,"Prefabs/od"),new Character(3,"绿萼",110,4,"Prefabs/le")
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

		//战斗开始事件
		EventCenter.GetInstance.addEventListener (BattleEvent.ATTACK, battleAttackHandler);
	}


	/// <summary>
	/// 战斗开始
	/// </summary>
	public void BattleStart ()
	{
		//Debug.Log ("Battle BattleStart");
		AddEnemy ();
		AddHero ();

		enemyList[0].AddTarget(soldierList[0]);
		soldierList[0].AddTarget(enemyList[0]);
	}

	/// <summary>
	/// 战斗结束
	/// </summary>
	private void BattleEnd ()
	{

 
		EventCenter.GetInstance.removeEventListener (BattleEvent.ATTACK, battleAttackHandler);
	}

	/// <summary>
	/// Adds the enemy.
	/// </summary>
	void AddEnemy ()
	{

		GameObject parent = StageManager.SharedInstance.NpcLayer;
		GameObject hfPrefab = ResourceManager.GetInstance.LoadPrefab (testDB[0].Prefab);
		GameObject hf = StageManager.SharedInstance.AddToStage (parent, hfPrefab);

		BaseSoldier hfSoldier = hf.AddComponent<HFSoldier>(); 
		BattleAgent agent = new BattleAgent(hfSoldier,testDB[0]);

		agent.BaseSprite.SetStagePosition (-350, 0);


		enemyList.Add (agent);

 
	}
	
	/// <summary>
	/// Adds the hero.
	/// </summary>
	void AddHero ()
	{
		GameObject parent = StageManager.SharedInstance.HeroLayer;
		GameObject odPrefab = ResourceManager.GetInstance.LoadPrefab (testDB[1].Prefab);
		GameObject od = StageManager.SharedInstance.AddToStage (parent, odPrefab);

		BaseSoldier odSoldier = od.AddComponent<ODSoldier> ();
		BattleAgent agent = new BattleAgent(odSoldier,testDB[1]);

		agent.BaseSprite.SetStagePosition (350, 0);
		soldierList.Add (agent);
	}

	void battleAttackHandler (CEvent e)
	{
		int index = Convert.ToInt32 (e.data);
		
		//奥丁大招 2000x 的技能ID
		if (index == 3) {
			soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Ult, null);
			AttackMessage message = new AttackMessage (soldierList [0], enemyList, 20001);
			soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		}
		

		if (index == 2) {
			soldierList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Attack, null);
			AttackMessage message = new AttackMessage (soldierList [0], enemyList, 1);
			soldierList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		}

		//黑风攻击
		if (index == 1) {
			enemyList [0].BaseSoldier.StateMachine.ToggleMajorState (StateId.Attack, null);

			AttackMessage message = new AttackMessage (enemyList [0], soldierList, 1);
			enemyList [0].dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		}

	}

	void battleStartHandler (CEvent e)
	{
		BattleStart ();
		//Debug.Log ("battleStartHandler");

	}


}
