﻿using System;

/// <summary>
/// 黑风
/// </summary>
public class HFSoldier : EnemySoldier
{


	/// <summary>
	/// 初始化默认声音
	/// </summary>
	override public void AddSoundDemo ()
	{
		this.BattleAgent.BaseSprite.AddSound (StateId.Attack, "hf_action_attack_01");
		this.BattleAgent.BaseSprite.AddSound (StateId.Dead, "dead_hf");
		this.BattleAgent.AddTimerDemo (new float[]{0.8f, 6.0f});
		this.BattleAgent.AddSkillDemo (CooldownType.Attack, SkillData.testData [1]);

	}


	
	/// <summary>
	/// Raises the shoot on event.
	/// </summary>
	override protected void OnShootOnEvent ()
	{
		
	}
 
	
	override protected void OnUltShootOnEvent ()
	{
		
	}
}
