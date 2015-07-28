using System;
using UnityEngine;

public class IdleState : MajorBaseState
{
	public IdleState ()
	{
		this.StateId = StateId.Idle;
		//Debug.Log ("IdleState()");

	}

	public override void OnEnter (object param)
	{
	}

	public override void OnExit ()
	{
		//Debug.Log ("IdleState OnExit");
	}

	public override void OnExecute (object param)
	{
		//Debug.Log ("IdleState OnExecute");
	}

	public override void HandleMessage (object param)
	{
		//Debug.Log ("IdleState HandleMessage");
	}
}

 
