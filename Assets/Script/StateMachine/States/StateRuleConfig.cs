using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 可配置状态切换规则
/// </summary>
public class StateRuleConfig
{
	private static StateRuleConfig instance;

	private StateRuleConfig ()
	{
		initConfig ();
	}

	public static StateRuleConfig GetInstance {
		get {
			if (instance == null) {

				instance = new StateRuleConfig ();
			}
			return instance;
		}
	}

	private Dictionary<StateId,ConfigData> dict = new Dictionary<StateId, ConfigData> ();

	public Dictionary<StateId, ConfigData> Dict {
		get {
			return dict;
		}
	}

	/// <summary>
	/// 初始化几个配置
	/// </summary>
	private void initConfig ()
	{
		dict.Add (StateId.Idle, new ConfigData (StateId.Idle, new List<StateId> () {
			(StateId)2,
			(StateId)3,
			(StateId)4,
			(StateId)5
		}, new List<StateId> (){ }));
		dict.Add (StateId.Attack, new ConfigData (StateId.Attack, new List<StateId> () {
			(StateId)1,
			(StateId)3,
			(StateId)4,
			(StateId)5
		}, new List<StateId> (){ (StateId)502 }));
		dict.Add (StateId.Ult, new ConfigData (StateId.Ult, new List<StateId> () {
			(StateId)1,
			(StateId)2,
			(StateId)4,
			(StateId)5
		}, new List<StateId> (){ (StateId)503 }));

	}

}


/// <summary>
/// 配置项目
/// </summary>
public class ConfigData
{

	public ConfigData (StateId key, List<StateId> toggle, List<StateId> exclude)
	{
		this.keyStateId = key;
		this.toggleMajorState = toggle;
		this.excludeSubState = exclude;
	}


	/// <summary>
	/// 主状态
	/// </summary>
	private StateId keyStateId = StateId.NullStateId;

	public StateId KeyStateId {
		get {
			return keyStateId;
		}
		set {
			keyStateId = value;
		}
	}

	/// <summary>
	/// 当前状态 可以切换的主状态
	/// </summary>
	private List<StateId> toggleMajorState = new List<StateId> ();

	public List<StateId> ToggleMajorState {
		get {
			return toggleMajorState;
		}
		set {
			toggleMajorState = value;
		}
	}

	/// <summary>
	/// 当前状态 发生排斥的子状态
	/// </summary>
	private List<StateId> excludeSubState = new List<StateId> ();


	public List<StateId> ExcludeSubState {
		get {
			return excludeSubState;
		}
		set {
			excludeSubState = value;
		}
	}
}