using System;

/// <summary>
/// 状态基类
/// </summary>
public abstract class BaseState
{


 
	private StateId stateId;

	public StateId StateId {
		get {
			return stateId;
		}
		set {
			stateId = value;
		}
	}
 

	/// <summary>
	/// 状态类型
	/// </summary>
	/// <value>The type of the state.</value>
	private StateType stateType;

	public StateType StateType {
		get {
			return stateType;
		}
		set {
			stateType = value;
		}
	}


	protected BaseState ()
	{
		this.stateId = StateId.NullStateId;
	}


	/// <summary>
	/// 进入状态
	/// </summary>
	/// <param name="param">Parameter.</param>
	public virtual void OnEnter (object param)
	{
		
	}

	/// <summary>
	/// 退出状态
	/// </summary>
	public virtual void OnExit ()
	{

	}


	/// <summary>
	/// 执行动作
	/// </summary>
	/// <param name="param">Parameter.</param>
	public virtual void OnExecute (object param)
	{

	}

	/// <summary>
	/// 状态处理对应消息
	/// </summary>
	/// <param name="param">Parameter.</param>
	public virtual void HandleMessage (object param)
	{
		
	}

}
	