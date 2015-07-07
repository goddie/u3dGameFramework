using System;
using UnityEngine;

/// <summary>
/// 代理控制器
/// 代理控制器关联着Sprit,StateMachine,Role,SkillEffect,Event等
/// </summary>
public class BattleAgent : MonoBehaviour
{
	public BattleAgent ()
	{
		this.StateMachine = new StateMachine ();
		this.Sprite = new Sprite ();

		//EventCenter.GetInstance.addEventListener();
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
	/// 动画
	/// </summary>
	private Sprite sprite;

	public Sprite Sprite {
		get {
			return sprite;
		}
		set {
			sprite = value;
		}
	} 

	public void HandleMessage (object param)
	{

		Debug.Log ("Soldier HandleMessage");

		CEvent e = (CEvent)param;

		ToggleState (StateId.Attack);

		this.stateMachine.HandleMessage (e.data);
	}

	/// <summary>
	/// 切换状态
	/// </summary>
	/// <param name="stateId">State identifier.</param>
	public void ToggleState (StateId stateId)
	{
		this.StateMachine.ToggleState (stateId, gameObject);
		this.Sprite.ToggleState (stateId);
	}

}