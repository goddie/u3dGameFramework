using System;
using System.Collections.Generic;

/// <summary>
/// 状态切换规则
/// </summary>
public class StateRule
{
	private static StateRule instance;

	private StateRule ()
	{
		this.StateRuleConfig = StateRuleConfig.GetInstance;
	}

	public static StateRule GetInstance {
		get {
			if (instance == null) {

				instance = new StateRule ();
			}
			return instance;
		}
	}


	public StateRuleConfig StateRuleConfig {
		get;
		set;
	}



	/// <summary>
	/// 是否可以切换状态
	/// </summary>
	/// <returns><c>true</c>, if can toggle was ised, <c>false</c> otherwise.</returns>
	/// <param name="currentStateId">当前状态</param>
	/// <param name="newStateId">新状态</param>
	/// <param name="subStateList">角色当前身上的子状态</param>
	public Boolean IsCanToggle (StateId currentStateId, StateId newStateId, List<BaseState> subStateList)
	{
		//是否是相同的主状态
		if (IsSameState (currentStateId, newStateId)) {
			return false;
		}

		//角色身上是否有与主状态互斥的子状态
		if (IsHaveMutexSubState (newStateId, subStateList)) {
			return false;
		}


		return IsCanToggleToNewState (currentStateId, newStateId);
	}

	/// <summary>
	/// 新状态是否与角色身上的子状态互斥 
	/// </summary>
	/// <returns><c>true</c>, if have mutex sub state was ised, <c>false</c> otherwise.</returns>
	/// <param name="newStateId">New state identifier.</param>
	/// <param name="subStateList">角色当前身上的子状态</param>
	public Boolean IsHaveMutexSubState (StateId newStateId, List<BaseState> subStateList)
	{
		//新状态的互斥子状态
		List<StateId> excludeList = StateRuleConfig.GetInstance.Dict [newStateId].ExcludeSubState;

		if (excludeList == null || excludeList.Count == 0) {
			return false;
		}

		for (int i = 0; i < excludeList.Count; i++) {

			StateId subStateId = excludeList [i];
			//包含互斥状态
			if (IsHaveSubState (subStateList, subStateId)) {
				return true;
			}
		}
			

		return false;
	}


	/// <summary>
	/// 包含指定子状态
	/// </summary>
	/// <returns><c>true</c>, if have sub state was ised, <c>false</c> otherwise.</returns>
	/// <param name="subStateList">Sub state list.</param>
	/// <param name="subStateId">Sub state identifier.</param>
	public Boolean IsHaveSubState (List<BaseState> subStateList, StateId subStateId)
	{

		for (int i = 0; i < subStateList.Count; i++) {

			if (subStateList [i].StateId == subStateId) {
			
				return true;
			
			}

		}

		return false;
	}


	/// <summary>
	/// 是否跟当前状态相同
	/// </summary>
	/// <returns><c>true</c>, if same state was ised, <c>false</c> otherwise.</returns>
	/// <param name="currentStateId">Current state identifier.</param>
	/// <param name="newStateId">New state identifier.</param>
	public Boolean IsSameState (StateId currentStateId, StateId newStateId)
	{
		return currentStateId == newStateId;
	}


	/// <summary>
	/// 能否切换到新状态 
	/// </summary>
	/// <returns><c>true</c>, if can toggle to new state was ised, <c>false</c> otherwise.</returns>
	/// <param name="currentStateId">Current state identifier.</param>
	/// <param name="newStateId">New state identifier.</param>
	private Boolean IsCanToggleToNewState (StateId currentStateId, StateId newStateId)
	{
		List<StateId> rules = StateRuleConfig.GetInstance.Dict [currentStateId].ToggleMajorState;

		if (rules == null || rules.Count == 0) {
		
			return false;
		}

		if (rules.Contains (newStateId)) {
		
			return true;
		}
 
		return false;
	}

	/// <summary>
	/// 是否是子状态
	/// </summary>
	/// <returns><c>true</c>, if sub state was ised, <c>false</c> otherwise.</returns>
	/// <param name="stateId">Current state identifier.</param>
	public Boolean IsSubState (StateId stateId)
	{
		return stateId > StateId.SubStateStartId;
	}
}
