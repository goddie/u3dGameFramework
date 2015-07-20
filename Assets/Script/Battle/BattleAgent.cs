using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 代理控制器
/// 可以接收分发事件
/// </summary>
using System.Collections;


public class BattleAgent : EventDispatcherBase
{


	public BattleAgent (BaseSoldier baseSoldier, Character character)
	{
		this.BaseSoldier = baseSoldier;
		this.gameObject = baseSoldier.gameObject;
		this.BaseSprite = baseSoldier.gameObject.AddComponent<BaseSprite>();
		this.Character = character;

		AddEventListeners ();
	}

	private void AddEventListeners ()
	{
		addEventListener (SoldierEvent.BATTLE_MESSAGE, HandleMessage);
		addEventListener (SoldierEvent.HIT, HitHandler);
	}




	/// <summary>
	/// 战斗信息处理
	/// </summary>
	/// <param name="e">E.</param>
	private void HandleMessage (CEvent e)
	{
		baseSoldier.HandleMessage (e);
	}


	/// <summary>
	/// 被击中
	/// </summary>
	/// <param name="e">E.</param>
	private void HitHandler (CEvent e)
	{
		//Debug.Log ("HitHandler");

		AttackMessage attackMessage = (AttackMessage)e.data;

		baseSprite.HitEffect(attackMessage.Sender);
	}


	private GameObject gameObject;

	public GameObject GameObject {
		get {
			return gameObject;
		}
	}
 

	/// <summary>
	/// 兵种
	/// </summary>
	private BaseSoldier baseSoldier;

	public BaseSoldier BaseSoldier {
		get {
			return baseSoldier;
		}
		set {
			baseSoldier = value;
			baseSoldier.BattleAgent = this;
		}
	}


	/// <summary>
	/// 动作相关
	/// </summary>
	private BaseSprite baseSprite;

	public BaseSprite BaseSprite {
		get {
			return baseSprite;
		}
		set {
			baseSprite = value;
			baseSprite.BattleAgent = this;
		}
	}

	/// <summary>
	/// 战斗目标
	/// </summary>
	private List<BattleAgent> targets;

	public List<BattleAgent> Targets {
		get {
			return targets;
		}
	}	



	/// <summary>
	/// 角色属性
	/// </summary>
	private Character character;

	public Character Character {
		get {
			return character;
		}
		set {
			character = value;
			character.BattleAgent = this;
			this.baseSprite.HitPoint = character.HitPoint;
			this.baseSprite.AttackPoint = character.AttackPoint;
		}
	}
	
	public void RemoveFromStage ()
	{
		clearEvents ();
	}

	/// <summary>
	/// 新增目标
	/// </summary>
	/// <param name="target">Target.</param>
	public void AddTarget (BattleAgent target)
	{
		if (this.targets == null) {
			this.targets = new List<BattleAgent> ();
		}
		this.targets.Add (target);
	}


}