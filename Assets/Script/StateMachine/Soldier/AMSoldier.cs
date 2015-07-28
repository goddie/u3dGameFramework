
using System;

/// <summary>
/// 阿莫
/// </summary>
using System.Collections.Generic;
using UnityEngine;


public class AMSoldier : BossSoldier
{
	private float speed = 1.0f;
	private BaseBullet baseBullet;
	
 
	
	
	override protected void OnShootOn ()
	{
 
	}
	
	
	
	
	override protected void OnUltShootOn ()
	{
		OnUltEnd();
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