using System;
using UnityEngine;

public class WalkState: MajorBaseState
{
	public WalkState ()
	{
		this.StateId = StateId.Walk;
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
