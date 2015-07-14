using System;
using UnityEngine;

public class BaseSoldier : MonoBehaviour ,IStateEvent
{



	void Awake ()
	{
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
		AttackMessage attackMessage = (AttackMessage)e.data;

		//大招Id大于20000
		if (attackMessage.SpellId > 20000) {

			stateMachine.ToggleState (StateId.Ult, e);
			battleAgent.BaseSprite.ToggleState (StateId.Ult);

		} else {
			stateMachine.ToggleState (StateId.Attack, e);
			battleAgent.BaseSprite.ToggleState (StateId.Attack);
		}

		//Debug.Log ("battleMessageHandler");

	}


	/// <summary>
	/// 动画事件触发
	/// BattleAgent监听
	/// </summary>
	/// <param name="keyId">Key identifier.</param>
	public void TriggerKeyEvent (KeyEventId keyId)
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

}
