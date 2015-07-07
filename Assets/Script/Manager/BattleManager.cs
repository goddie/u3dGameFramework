using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 战斗管理
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
		//战斗开始事件
		EventCenter.GetInstance.addEventListener (BattleEvent.START, battleStartHandler);
		EventCenter.GetInstance.addEventListener (BattleEvent.ATTACK, battleAttackHandler);
	}


	/// <summary>
	/// 战斗开始
	/// </summary>
	private void BattleStart ()
	{
		//Debug.Log ("Battle BattleStart");
		AddEnemy ();
		AddHero ();
	}

	/// <summary>
	/// 战斗结束
	/// </summary>
	private void BattleEnd ()
	{
		EventCenter.GetInstance.removeEventListener (BattleEvent.START, battleStartHandler);
		EventCenter.GetInstance.removeEventListener (BattleEvent.ATTACK, battleAttackHandler);
	}

	/// <summary>
	/// Adds the enemy.
	/// </summary>
	void AddEnemy ()
	{
		GameObject hfPrefab = ResourceManager.GetInstance.LoadPrefab ("Prefabs/hf");
		GameObject parent = StageManager.SharedInstance.NpcLayer;
		//		Debug.Log ("Battle AddEnemy");
		GameObject hf = StageManager.SharedInstance.AddToStage (parent, hfPrefab);
		BattleAgent soldier = hf.AddComponent<HFAgent> ();
		//sprite.SetStagePosition (213, -55);
		enemyList.Add (soldier);
	}
	
	/// <summary>
	/// Adds the hero.
	/// </summary>
	void AddHero ()
	{
		GameObject odPrefab = ResourceManager.GetInstance.LoadPrefab ("Prefabs/od");
		GameObject parent = StageManager.SharedInstance.HeroLayer;
		GameObject od = StageManager.SharedInstance.AddToStage (parent, odPrefab);
		BattleAgent soldier = od.AddComponent<ODAgent> ();
		soldier.Sprite.SetStagePosition (213, -55);
		
		soldierList.Add (soldier);
	}



	void battleAttackHandler (CEvent e)
	{
		int index = Convert.ToInt32 (e.data);
		
		
		if (index == 2) {
			soldierList [0].StateMachine.ToggleMajorState (StateId.Attack, null);
			soldierList [0].Sprite.dispatchEvent (SoldierEvent.ATTACK, soldierList [0].StateMachine.CurrentState.StateId);
		}
		
		if (index == 1) {
			enemyList [0].StateMachine.ToggleMajorState (StateId.Attack, null);
			enemyList [0].Sprite.dispatchEvent (SoldierEvent.ATTACK, enemyList [0].StateMachine.CurrentState.StateId);
		}

	}

	void battleStartHandler (CEvent e)
	{
		BattleStart ();
		Debug.Log ("battleStartHandler");

	}


}
