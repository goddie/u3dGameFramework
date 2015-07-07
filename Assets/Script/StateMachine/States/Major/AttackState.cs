using System;
using UnityEngine;

/// <summary>
/// 攻击
/// </summary>
public class AttackState : MajorBaseState
{
	public AttackState ()
	{
		this.StateId = StateId.Attack;
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

	}

	public override void HandleMessage (object param)
	{
		base.HandleMessage (param);
	}
}