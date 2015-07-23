using System;
using UnityEngine;
using System.Collections.Generic;

public class BaseSoldier : MonoBehaviour
{



	/// <summary>
	/// 战斗消息
	/// </summary>
	protected AttackMessage attackMessage;
	
	
	/// <summary>
	/// 战斗代理
	/// </summary>
	
	private BattleAgent battleAgent;

	public BattleAgent BattleAgent {
		get {
			return battleAgent;
		}
		set {
			battleAgent = value;
		}
	}	 

	void Awake ()
	{
		this.StateMachine = new StateMachine ();
	}
	
	/// <summary>
	/// 状态机
	/// </summary>
	private StateMachine stateMachine;

	public StateMachine StateMachine {
		get {
			return stateMachine;
		}
		set {
			stateMachine = value;
		}
	} 


	/// <summary>
	/// 动画事件触发
	/// BattleAgent监听
	/// </summary>
	/// <param name="keyId">Key identifier.</param>
	public void TriggerKeyEvent (KeyEventId keyId)
	{
		if (keyId == KeyEventId.AttackOn) {
			this.OnAttackOn ();
		}

		if (keyId == KeyEventId.ShootOn) {
			this.OnShootOn ();
		}


		if (keyId == KeyEventId.UltShootOn) {
			this.OnUltShootOn ();
		}
	}

	
	/// <summary>
	/// Raises the shoot on event.
	/// </summary>
	protected virtual void OnShootOn ()
	{
	
	}

	/// <summary>
	/// Raises the attack on event.
	/// </summary>
	protected virtual void OnAttackOn ()
	{
		this.OnAttackEnd ();
	}


	protected virtual void OnUltShootOn ()
	{

	}





	/// <summary>
	/// 待机
	/// </summary>
	public void OnIdle ()
	{
		ToggleState (StateId.Idle);
	}


	/// <summary>
	/// 行走
	/// </summary>
	public void OnWalk ()
	{
		ToggleState (StateId.Walk);
	}

	public bool IsIdle ()
	{
		return IsState (StateId.Idle);
	}

	/// <summary>
	/// 行走结束
	/// </summary>
	public void OnWalkEnd ()
	{
		ToggleState (StateId.Idle);
	}

	/// <summary>
	/// 是否正在行走
	/// </summary>
	/// <returns><c>true</c> if this instance is walk; otherwise, <c>false</c>.</returns>
	public bool IsWalk ()
	{
		return IsState (StateId.Walk);
	}

	/// <summary>
	/// 攻击
	/// </summary>
	public void OnAttack ()
	{
		ToggleState (StateId.Attack);
		battleAgent.BaseSprite.ToggleState (StateId.Attack);
		battleAgent.BaseSprite.PlaySound (StateId.Attack);
	}

	/// <summary>
	/// 攻击结束
	/// </summary>
	public void OnAttackEnd ()
	{

		for (int i = 0; i < battleAgent.Targets.Count; i++) {
			BattleAgent t = battleAgent.Targets [i];
			AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
			t.dispatchEvent (SoldierEvent.HIT, message);
		}

		ToggleState (StateId.Idle);
	}

	/// <summary>
	/// 是否正在攻击
	/// </summary>
	/// <returns><c>true</c> if this instance is attack; otherwise, <c>false</c>.</returns>
	public bool IsAttack ()
	{
		return IsState (StateId.Attack);
	}

	/// <summary>
	/// 放大招
	/// </summary>
	public void OnUlt ()
	{
		ToggleState (StateId.Ult);
		battleAgent.BaseSprite.ToggleState (StateId.Ult);
		battleAgent.BaseSprite.PlaySound (StateId.Ult);
	}
	
	/// <summary>
	/// 大招结束
	/// </summary>
	public void OnUltEnd ()
	{
		ToggleState (StateId.Idle);
	}
	
	/// <summary>
	/// 是否在大招
	/// </summary>
	/// <returns><c>true</c> if this instance is attack; otherwise, <c>false</c>.</returns>
	public bool IsUlt ()
	{
		return IsState (StateId.Ult);
	}




	/// <summary>
	/// Determines whether this instance is sate the specified stateId.
	/// </summary>
	/// <returns><c>true</c> if this instance is sate the specified stateId; otherwise, <c>false</c>.</returns>
	/// <param name="stateId">State identifier.</param>
	public bool IsState (StateId stateId)
	{
		return stateMachine.IsState (stateId);
	}

	/// <summary>
	/// 切换状态
	/// </summary>
	/// <param name="stateId">State identifier.</param>
	public void ToggleState (StateId stateId)
	{
		stateMachine.ToggleState (stateId, this.attackMessage);
	}


	public virtual void AddSoundDemo ()
	{
		battleAgent.BaseSprite.AddSound (StateId.Attack, "attack_od");
		battleAgent.BaseSprite.AddSound (StateId.Ult, "ult_od");
		battleAgent.BaseSprite.AddSound (StateId.Dead, "dead_od");

	}
	
//	/// <summary>
//	/// 计时器初始化
//	/// </summary>
//	public virtual void AddTimerDemo ()
//	{
//		attackTimer = TimerManager.SharedInstance.CreateTimer (2.0f, new TimerEventHandler (AttackHandler));
//		ultTimer = TimerManager.SharedInstance.CreateTimer (6.0f, new TimerEventHandler (UltHandler));
//		
//		attackTimer.Start ();
//		ultTimer.Start ();
//	}

}
