
using System;

/// <summary>
/// 阿莫
/// </summary>
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AMSoldier : BossSoldier
{
	private float speed = 1.0f;
	private BaseBullet baseBullet;
	
	override protected void OnShootOnEvent ()
	{
 
	}
	
	override protected void OnUltShootOnEvent ()
	{

	}

	override protected void OnFloatEvent ()
	{

		StartCoroutine(FloatWait());
	}


	/// <summary>
	/// 浮空等待
	/// </summary>
	/// <returns>The wait.</returns>
	IEnumerator FloatWait ()
	{
		Animator animator = this.gameObject.GetComponent<Animator> ();
		animator.enabled = false;

		yield return new WaitForSeconds (SkillData.FLOAT_TIME);

		animator.enabled = true;

		this.OnIdle();
	}
	
	
	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		this.BattleAgent.BaseSprite.AddSound (StateId.Attack, "attack_od");
		this.BattleAgent.BaseSprite.AddSound (StateId.Ult, "ult_od");
		this.BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_od");
		
		this.BattleAgent.AddTimerDemo (new float[]{1.8f, 5});
		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [3]);
		this.BattleAgent.AddSkillDemo (CooldownType.Ult, SkillData.testData [20006]);
	}
	

}