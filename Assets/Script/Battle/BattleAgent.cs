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
	private AttackMessage attackMessage;

	private Dictionary<CooldownType,CooldownTimer> timerDict = new Dictionary<CooldownType, CooldownTimer> ();

	public BattleAgent (BaseSoldier baseSoldier, Character character)
	{
		this.BaseSoldier = baseSoldier;

		this.gameObject = baseSoldier.gameObject;
		this.BaseSprite = baseSoldier.gameObject.AddComponent<BaseSprite> ();
		this.Character = character;

		this.baseSoldier.AddSoundDemo ();
		//AddTimer ();

		AddEventListeners ();
	}

	private void AddEventListeners ()
	{
		addEventListener (SoldierEvent.BATTLE_MESSAGE, HandleMessage);
		addEventListener (SoldierEvent.HIT, HitHandler);
	}


	public void HandleMessage (CEvent e)
	{
		this.attackMessage = (AttackMessage)e.data;
		//大招 Id大于20000
		if (attackMessage.SpellId > 20000) {
			baseSoldier.OnUlt ();
		} else {
			baseSoldier.OnAttack ();
		}
		//Debug.Log ("battleMessageHandler");
	}

	/// <summary>
	/// 被击中
	/// </summary>
	/// <param name="e">E.</param>
	private void HitHandler (CEvent e)
	{
		//Debug.Log ("HitHandler");
		AttackMessage attackMessage = (AttackMessage)e.data;
		baseSprite.HitEffect (attackMessage.Sender);
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

	/// <summary>
	/// 计时器初始化
	/// </summary>
	private void AddTimer ()
	{
		AddTimer (CooldownType.Attack, 2.0f, new TimerEventHandler (AttackHandler));
		AddTimer (CooldownType.Ult, 6.0f, new TimerEventHandler (UltHandler));
	}

	private void AddTimer (CooldownType type, float second, TimerEventHandler action)
	{
		CooldownTimer t = TimerManager.SharedInstance.CreateTimer (second, action);
		timerDict.Add (type, t);
		t.Start ();
	}


	/// <summary>
	/// 攻击CD时调用
	/// </summary>
	protected void AttackHandler ()
	{
		if (this.Targets == null || this.Targets.Count == 0) {
			return;
		}
		
		AttackMessage message = new AttackMessage (this, BattleManager.SharedInstance.GetEnemyList (), 1);
		this.dispatchEvent (SoldierEvent.BATTLE_MESSAGE, message);
		
		//Debug.Log ("attackHandler");
	}
	
	
	/// <summary>
	/// 大招CD时调用
	/// </summary>
	protected void UltHandler ()
	{
		Debug.Log ("ultHandler");
	}

	
	public void AddTimerDemo (float[] timeList)
	{
		AddTimer (CooldownType.Attack, timeList [0], new TimerEventHandler (AttackHandler));
		AddTimer (CooldownType.Ult, timeList [1], new TimerEventHandler (UltHandler));
	}

}