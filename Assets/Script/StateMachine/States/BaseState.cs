using System;

/// <summary>
/// 状态基类
/// </summary>
public abstract class BaseState
{


	public BattleAgent BaseController {
		get;
		set;
	}


	public StateId StateId {
		get;
		set;
	}

	/// <summary>
	/// 状态类型
	/// </summary>
	/// <value>The type of the state.</value>
	public StateType StateType {
		get;
		set;
	}


	protected BaseState ()
	{
		this.StateId = StateId.NullStateId;
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
	