using System;
using UnityEngine;

/// <summary>
/// 只播放1次的动画
/// </summary>
public class NotLoopBehaviour : StateMachineBehaviour
{
 	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		 
	}

	public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		 
	}

	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("OnStateExit");

		BaseSoldier baseSoldier = animator.gameObject.GetComponent<BaseSoldier>();
		baseSoldier.BattleAgent.BaseSprite.ToggleState(StateId.Idle);

	}

	public override void OnStateMove (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		 
	}

	public override void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		 
	}

	public override void OnStateMachineEnter (Animator animator, int stateMachinePathHash)
	{
		 
	}

	public override void OnStateMachineExit (Animator animator, int stateMachinePathHash)
	{
		 
	}
}
