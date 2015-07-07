using System;

/// <summary>
/// 浮空状态
/// </summary>
public class FloatState: SubBaseState
{
	public FloatState ()
	{
		this.StateId = StateId.Float;
	}

	public override void OnEnter (object param)
	{
		base.OnEnter (param);
	}

	public override void OnExit ()
	{
		base.OnExit ();
	}

	public override void OnExecute (object param)
	{
		base.OnExecute (param);
	}

	public override void HandleMessage (object param)
	{
		base.HandleMessage (param);
	}
}
