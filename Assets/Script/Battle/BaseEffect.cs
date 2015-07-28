using System;
using UnityEngine;
using System.Collections;

public class BaseEffect : BaseAnim
{

	
	/// <summary>
	/// 动画事件触发
	/// </summary>
	/// <param name="keyId">Key identifier.</param>
	public void TriggerKeyEvent (KeyEventId keyId)
	{
		//IsState (StateId.Idle);
		
		if (keyId == KeyEventId.StateEnd) {
	
			RemoveAnimator ();
		}

		if (keyId == KeyEventId.ComboEnd) {

			FadeOutAnimator();
		}

	}


	/// <summary>
	/// 在士兵身上播放大招引导特效
	/// </summary>
	/// <param name="message">Message.</param>
	public void PlayOnAgent (AttackMessage message)
	{
		this.attackMessage = message;

		StartCoroutine(PlayOneShot(StateId.Idle));
	}

	

}