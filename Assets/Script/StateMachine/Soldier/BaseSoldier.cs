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
			this.OnAttackOnEvent ();
		}

		if (keyId == KeyEventId.ShootOn) {
			this.OnShootOnEvent ();
		}

		if (keyId == KeyEventId.UltShootOn) {
			this.OnUltShootOnEvent ();
		}

		if (keyId == KeyEventId.UltEnd) {
			this.OnUltEndEvent ();
		}

		if (keyId == KeyEventId.FloatOn) {
			this.OnFloatEvent ();
		}

		if (keyId == KeyEventId.FloatEnd) {
			this.OnFloatEndEvent ();
		}
	}

	
	/// <summary>
	/// 发射事件
	/// </summary>
	protected virtual void OnShootOnEvent ()
	{
	
	}

	/// <summary>
	/// 击中事件
	/// </summary>
	protected virtual void OnAttackOnEvent ()
	{
		this.OnAttackEnd ();
	}

	/// <summary>
	/// 大招发射事件
	/// </summary>
	protected virtual void OnUltShootOnEvent ()
	{

	}

	/// <summary>
	/// 大招结束
	/// </summary>
	protected virtual void OnUltEndEvent ()
	{
		this.OnUltEnd ();
	}

	/// <summary>
	/// 浮空
	/// </summary>
	protected virtual void OnFloatEvent ()
	{

	}


	/// <summary>
	/// 浮空结束
	/// </summary>
	protected virtual void OnFloatEndEvent ()
	{
		this.OnIdle ();
	}



	/// <summary>
	/// 待机
	/// </summary>
	public void OnIdle ()
	{
		ToggleState (StateId.Idle);
		if (IsIdle ()) {
			battleAgent.BaseSprite.ToggleState (StateId.Idle);
		}
	}


	/// <summary>
	/// 行走
	/// </summary>
	public void OnWalk ()
	{
		ToggleState (StateId.Walk);
		if (IsWalk ()) {
			battleAgent.BaseSprite.ToggleState (StateId.Walk);
		}
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
		this.battleAgent.FinishMove ();
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

		if (IsAttack () || IsWalk ()) {
			battleAgent.BaseSprite.ToggleState (StateId.Attack);
			battleAgent.BaseSprite.PlaySound (StateId.Attack);	
		}

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

		OnIdle ();

		this.battleAgent.FinishMove ();
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

		if (IsUlt ()) {
			//移到最顶上
			gameObject.transform.parent = StageManager.SharedInstance.MaskLayer.gameObject.transform;

			battleAgent.BaseSprite.ToggleState (StateId.Ult);
			battleAgent.BaseSprite.PlaySound (StateId.Ult);
		}

	}
	
	/// <summary>
	/// 大招结束
	/// </summary>
	public void OnUltEnd ()
	{
		ToggleState (StateId.Idle);

		if (this.GetType ().IsSubclassOf (typeof(HeroSoldier))) {
			gameObject.transform.parent = StageManager.SharedInstance.HeroLayer.gameObject.transform;
		} else {
			gameObject.transform.parent = StageManager.SharedInstance.NpcLayer.gameObject.transform;
		}

		//Debug.Log("OnUltEnd");
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
	/// 正在浮空
	/// </summary>
	public void OnFloat ()
	{
		ToggleState (StateId.Float);
//		Debug.Log(stateMachine.CurrentState.StateId);
		if (IsFloat ()) {
			battleAgent.BaseSprite.ToggleState (StateId.Float);
		}

	}

	public bool IsFloat ()
	{
		return IsState (StateId.Float);
	}

	/// <summary>
	/// 浮空结束
	/// </summary>
	public void OnFloatEnd ()
	{
		ToggleState (StateId.Idle);
	}
 

	/// <summary>
	/// 惊讶状态
	/// </summary>
	public void OnSurprise ()
	{
		ToggleState (StateId.Surprise);

		if (IsSurprise()) {
			battleAgent.BaseSprite.ToggleState (StateId.Surprise);
		}
	}

	/// <summary>
	/// 惊讶状态结束
	/// </summary>
	public void OnSurpriseEnd ()
	{
		ToggleState (StateId.Idle);
	}


	/// <summary>
	/// 惊讶状态
	/// </summary>
	public bool IsSurprise ()
	{
		
		return IsState (StateId.Surprise);
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
