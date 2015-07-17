using System;
using UnityEngine;
using System.Collections.Generic;

public class BaseSoldier : BasePlayer
{
	/// <summary>
	/// 声音配置
	/// </summary>
	protected Dictionary<StateId,String> soundDict = new Dictionary<StateId, string>();

	void Awake ()
	{
		InitSound();

		this.stateMachine = new StateMachine ();

	}


	/// <summary>
	/// 战斗代理
	/// </summary>
	protected BattleAgent battleAgent;

	public BattleAgent BattleAgent {
		get {
			return battleAgent;
		}
		set {
			battleAgent = value;
		}
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

	public void HandleMessage (CEvent e)
	{
		this.attackMessage = (AttackMessage)e.data;

		//大招 Id大于20000
		if (attackMessage.SpellId > 20000) {
//			stateMachine.ToggleState (StateId.UltWait, e);
//			battleAgent.BaseSprite.ToggleState (StateId.UltWait);

			stateMachine.ToggleState (StateId.Ult, e);
			battleAgent.BaseSprite.ToggleState (StateId.Ult);

			PlaySound(StateId.Ult);

		} else {
			stateMachine.ToggleState (StateId.Attack, e);
			battleAgent.BaseSprite.ToggleState (StateId.Attack);
			PlaySound(StateId.Attack);
		}




		//Debug.Log ("battleMessageHandler");

	}


	/// <summary>
	/// 动画事件触发
	/// BattleAgent监听
	/// </summary>
	/// <param name="keyId">Key identifier.</param>
	public virtual void TriggerKeyEvent (KeyEventId keyId)
	{
		if (keyId == KeyEventId.AttackOn) {

			Debug.Log ("BaseSoldier AttackOn");

			for (int i = 0; i < battleAgent.Targets.Count; i++) {
				BattleAgent t = battleAgent.Targets [i];
				AttackMessage message = new AttackMessage (battleAgent, battleAgent.Targets, 1);
				t.dispatchEvent (SoldierEvent.HIT, message);
			}
		}
	}


	/// <summary>
	/// 播放状态声音
	/// </summary>
	/// <param name="stateId">State identifier.</param>
	private void PlaySound(StateId stateId)
	{
		Debug.Log("PlaySound:"+stateId);
		if (!soundDict.ContainsKey(stateId)) {
			Debug.Log("No Sound:"+stateId);
			return;	
		}
		
		String soundName = soundDict[stateId];
		AudioManager.SharedInstance.FMODEvent(soundName,1.0f );
	}



	/// <summary>
	/// 初始化默认声音
	/// </summary>
	protected virtual void InitSound()
	{
		soundDict.Add(StateId.Attack,"attack_od");
		soundDict.Add(StateId.Ult,"ult_od");
		soundDict.Add(StateId.Dead,"dead_od");
	}

}
