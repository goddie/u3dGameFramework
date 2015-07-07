using System;
using UnityEngine;
using System.Collections.Generic;

public class StateMachine
{
	public StateMachine ()
	{
		InitDefaultState ();
	}

	private List<BaseState> subStateList = new List<BaseState> ();


	/// <summary>
	/// 兵种
	/// </summary>
	/// <value>The soldier.</value>
	public BattleAgent Soldier {
		get;
		set;
	}

	/// <summary>
	/// 当前状态
	/// </summary>
	/// <value>The state of the current.</value>

	public BaseState CurrentState {
		get;
		set;
	}

	/// <summary>
	/// 状态切换规则
	/// </summary>
	/// <value>The state rule.</value>
	public StateRule StateRule {
		get;
		set;
	}

	/// <summary>
	/// 初始化默认状态
	/// </summary>
	private void InitDefaultState ()
	{
		CurrentState = CreateState (StateId.Idle);
		StateRule = StateRule.GetInstance;
		Debug.Log ("初始化默认状态为 " + CurrentState.ToString ());
	}



	/// <summary>
	/// 创建状态
	/// </summary>
	private BaseState CreateState (StateId newStateId)
	{
		BaseState newState = StateCreator.GetInstance.CreateState (newStateId);

		if (newState != null) {

			newState.BaseController = this.Soldier;

		}

		return newState;
	}



		

	/// <summary>
	/// 是否是子状态，如果是，则直接加到子状态列表
	/// </summary>
	public void ToggleState (StateId newStateId, object param)
	{
		if (this.StateRule.IsSubState (newStateId)) {
			AddSubState (newStateId, param);
		} else {
			ToggleMajorState (newStateId, param);
		}
	}


	/// <summary>
	/// 切换主状态
	/// </summary>
	public void ToggleMajorState (StateId newStateId, object param)
	{

		if (!this.StateRule.IsCanToggle (CurrentState.StateId, newStateId, this.subStateList)) {
			//toggleStateFail
			return;
		}

		//没有对应的状态，直接返回
		BaseState newState = CreateState (newStateId);
		if (newState == null) {
			//toggleStateFail
			return;
		}

		//退出当前状态  
		CurrentState.OnExit ();
		this.CurrentState = newState;

		newState.OnEnter (param);

	}

 


	/// <summary>
	/// 新增子状态
	/// </summary>
	/// <param name="newStateId">New state identifier.</param>
	/// <param name="param">Data.</param>
	public void AddSubState (StateId newStateId, object param)
	{
		BaseState subState = CreateState (newStateId);
		if (subState == null) {
			//addSubStateFail ();
			return;
		}

		subState.OnEnter (param);
		InsertSubState (subState);

	}


	public void InsertSubState (BaseState subState)
	{
		subStateList.Add (subState);
	}


	public void RemoveSubState (StateId subStateId)
	{
		for (int i = 0; i < subStateList.Count; i++) {

			if (subStateList [i].StateId == subStateId) {

				subStateList [i].OnExit ();

				subStateList.RemoveAt (i);
			}

		}
	}

	public Boolean IsState (StateId stateId)
	{
 
		return CurrentState.StateId == stateId;  
  
	}

	public void HandleMessage (object param)
	{
		for (int i = 0; i < subStateList.Count; i++) {
			BaseState sb = subStateList [i];
			sb.HandleMessage (param);
		}

		CurrentState.HandleMessage (param);
	}

	public void Update (object param)
	{
		this.CurrentState.OnExecute (param);
	}

}